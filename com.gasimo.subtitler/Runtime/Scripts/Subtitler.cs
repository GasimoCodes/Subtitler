using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace Gasimo.Subtitles
{
    [RequireComponent(typeof(UIDocument))]
    [HelpURL(siteURL + "manual/Components/subtitler.html")]
    public class Subtitler : MonoSingleton<Subtitler>
    {
        public const string siteURL = "https://gasimo.dev/Subtitler/";

        [Header("Appearance")]
        [SerializeField] private Color speakerHighlight = Color.white;
        [SerializeField] private Color backgroundColor = new Color(0,0,0, 0.3137255f);
        [SerializeField] private bool enableBackgroundPanel = true;
        [SerializeField] private bool lineBreakSpeaker = false;
        [Space(5)]
        [SerializeField] private Font subtitleFont;
        [SerializeField] private TextAnchor subtitleAlign = TextAnchor.MiddleLeft;
        [SerializeField] private int subtitleSize = 24;
        [SerializeField] private SubtitlerTransitionBase transitionData;

        [SerializeField] private int subtitlePoolSize = 10;

        [Header("References")]
        [SerializeField] private AudioListener player;

        private UIDocument uiDocument;
        private VisualElement displayPanel;
        private ObjectPool<Label> subtitlePool;
        private Dictionary<int, (CancellationTokenSource, IAudioPlayer)> activeSubtitles = new();
        private int activeSubtitleCount = 0;
        private int nextSubtitleId = 0;
        private bool isInitialized = false;
        private ISubtitlerTransition transition;
        

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        private void Start()
        {
            if (player == null)
            {
                Debug.LogWarning("Player object not assigned, range-limited subtitles will fail. Attempting to find AudioListener.");
                player = FindAnyObjectByType<AudioListener>();
                if (player == null)
                {
                    Debug.LogError("Failed to find AudioListener. Range-limited subtitles will not work.");
                }
            }
        }

        private void Initialize()
        {
            if (isInitialized) return;

            uiDocument = GetComponent<UIDocument>();
            InitializeDisplayPanel();
            InitializeSubtitlePool();
            InitializeTransitions();
            isInitialized = true;
        }

        private void InitializeDisplayPanel()
        {
            displayPanel = uiDocument.rootVisualElement.Q<VisualElement>("SubtitlePanel");
            if (displayPanel == null)
            {
                Debug.LogError("SubtitlePanel not found in UIDocument. Please ensure it exists in your UI layout.");
                return;
            }

            displayPanel.usageHints = UsageHints.GroupTransform;
            displayPanel.style.visibility = Visibility.Hidden;
            displayPanel.style.backgroundColor = backgroundColor;
            displayPanel.Clear();
        }

        private void InitializeSubtitlePool()
        {
            subtitlePool = new ObjectPool<Label>(
                createFunc: CreateSubtitleLabel,
                actionOnGet: PrepareSubtitleLabel,
                actionOnRelease: ReleaseSubtitleLabel,
                actionOnDestroy: DestroySubtitleLabel,
                collectionCheck: false,
                defaultCapacity: 5,
                maxSize: subtitlePoolSize
            );
        }

        private void InitializeTransitions()
        {
            if (transitionData != null)
            {
                transition = transitionData;
            }
            else
            {
                Debug.LogWarning("No transition ScriptableObject set for Subtitler. Using default transition.");
                transition = ScriptableObject.CreateInstance<SubtitlerFadeTransition>();
            }
        }

        public void Update()
        {
            foreach (var entry in activeSubtitles)
            {
                if (entry.Value.Item2 == null) continue;
                entry.Value.Item2.SetTimeScale(UnityEngine.Time.timeScale);
            }
        }

        #region PoolFunctions

        private Label CreateSubtitleLabel()
        {
            var label = new Label
            {
                enableRichText = true,
                usageHints = UsageHints.DynamicTransform
            };

            transition.OnLabelCreated(label);

            label.style.fontSize = subtitleSize;

            if (subtitleFont != null)
                label.style.unityFontDefinition = new(subtitleFont);

            label.style.flexWrap = Wrap.Wrap;
            label.style.unityTextAlign = new StyleEnum<TextAnchor>(subtitleAlign);
            
            return label;
        }

        private void PrepareSubtitleLabel(Label label)
        {
            displayPanel.Add(label);
            label.text = string.Empty;
            label.style.visibility = Visibility.Visible;
        }

        private void ReleaseSubtitleLabel(Label label)
        {
            displayPanel.Remove(label);
            label.text = string.Empty;
            label.style.visibility = Visibility.Hidden;
        }

        private void DestroySubtitleLabel(Label label)
        {
            label.RemoveFromHierarchy();
        }

        #endregion

        /// <summary>
        /// Plays a sequence of SubtitleEntries on a given AudioSource
        /// </summary>
        /// <param name="sequenceData">Sequence to be played</param>
        /// <param name="audioSource">AudioSource to play through</param>
        /// <returns>Id of the session instance</returns>
        public int PlaySubtitleSequence(SubtitleSequenceData sequenceData, AudioSource audioSource)
        {
            Initialize();
            int id = GetNextSubtitleId();
            IAudioPlayer audio = new UnityAudioAdapter(audioSource, player);
            var cts = new CancellationTokenSource();
            activeSubtitles[id] = (cts, audio);
            _ = PlaySubtitleSequenceAsync(sequenceData, audio, id, cts.Token);
            return id;
        }

        /// <summary>
        /// Plays a sequence of SubtitleEntries on a given IAudioPlayer
        /// </summary>
        /// <param name="sequenceData">Sequence to be played</param>
        /// <param name="audioPlayer">IAudioPlayer to play through</param>
        /// <returns>Id of the session instance</returns>
        public int PlaySubtitleSequence(SubtitleSequenceData sequenceData, IAudioPlayer audioPlayer)
        {
            Initialize();
            int id = GetNextSubtitleId();
            var cts = new CancellationTokenSource();
            activeSubtitles[id] = (cts, audioPlayer);
            _ = PlaySubtitleSequenceAsync(sequenceData, audioPlayer, id, cts.Token);
            return id;
        }

        /// <summary>
        /// Plays a single line of subtitles on a given AudioSource
        /// </summary>
        /// <param name="entry">Entry containing the subtitle data</param>
        /// <param name="audioSource">AudioSource to play through</param>
        /// <returns>Id of the session instance</returns>
        public int PlaySubtitleEntry(ISubtitleEntry entry, AudioSource audioSource)
        {
            Initialize();
            int id = GetNextSubtitleId();
            IAudioPlayer audio = new UnityAudioAdapter(audioSource, player);
            var cts = new CancellationTokenSource();
            activeSubtitles[id] = (cts, audio);
            _ = PlaySubtitleEntryAsync(entry, audio, cts.Token);
            return id;
        }

        /// <summary>
        /// Plays a single line of subtitles on a given AudioPlayer
        /// </summary>
        /// <param name="entry">Entry containing the subtitle data</param>
        /// <param name="audioPlayer">AudioPlayer to play through</param>
        /// <returns>Id of the session instance</returns>
        public int PlaySubtitleEntry(ISubtitleEntry entry, IAudioPlayer audioPlayer)
        {
            Initialize();
            int id = GetNextSubtitleId();
            var cts = new CancellationTokenSource();
            activeSubtitles[id] = (cts, audioPlayer);
            _ = PlaySubtitleEntryAsync(entry, audioPlayer, cts.Token);
            return id;
        }

        /// <summary>
        /// Plays a single line of subtitles
        /// </summary>
        /// <param name="entry">Entry containing the subtitle data</param>
        /// <returns>Id of the session instance</returns>
        public int PlaySubtitleEntry(ISubtitleEntry entry)
        {
            Initialize();
            int id = GetNextSubtitleId();
            var cts = new CancellationTokenSource();
            activeSubtitles[id] = (cts, null);
            _ = PlaySubtitleEntryAsync(entry, null, cts.Token);
            return id;
        }



        /// <summary>
        /// Removes and hides a Subtitle session immediately.
        /// </summary>
        /// <param name="id">Id of the session to hide</param>
        public void RemoveSubtitle(int id)
        {
            if (activeSubtitles.TryGetValue(id, out var cts))
            {
                cts.Item1.Cancel();
                cts.Item1.Dispose();
                activeSubtitles.Remove(id);
            }
        }

        /// <summary>
        /// Removes and hides a active Subtitle Session with the oldest id.
        /// </summary>
        public void RemoveOldest()
        {
            if (activeSubtitles.Count > 0)
            {
                int oldestSubtitleId = activeSubtitles.Keys.Min();
                RemoveSubtitle(oldestSubtitleId);
            }
        }

        private async Task PlaySubtitleSequenceAsync(SubtitleSequenceData sequenceData, IAudioPlayer audioSource, int id, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var entry in sequenceData.Subtitles)
                {
                    await WaitForGameTime(entry.waitFor, cancellationToken);
                    _ = PlaySubtitleEntryAsync(entry, audioSource, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                // Subtitle sequence was cancelled, clean up if necessary
            }
            finally
            {
                activeSubtitles.Remove(id);
            }
        }

        private async Task PlaySubtitleEntryAsync(ISubtitleEntry entry, IAudioPlayer audioSource, CancellationToken cancellationToken)
        {
            if (audioSource != null && entry.getAudio() != null)
            {
                audioSource.PlayOneShot(entry.getAudio());
            }

            if (!audioSource.IsHearable()) return;

            entry.getSubtitleEvent()?.Raise();

            if (!string.IsNullOrEmpty(entry.getDialogue()))
            {
                await DisplaySubtitleAsync(entry, cancellationToken);
            }
        }

        private async Task DisplaySubtitleAsync(ISubtitleEntry entry, CancellationToken cancellationToken)
        {
            var subtitle = subtitlePool.Get();
            activeSubtitleCount++;
            UpdateSubtitlePanelVisibility();

            try
            {
                SetSubtitleText(subtitle, entry.getSpeaker(), entry.getDialogue());

                // Measure the time taken by the entrance animation
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                await transition.AnimateSubtitleEntrance(subtitle, entry, cancellationToken);
                stopwatch.Stop();

                // Calculate remaining display time and delay
                var remainingDisplayTime = TimeSpan.FromSeconds(entry.getDisplayFor()) - stopwatch.Elapsed;

                if (remainingDisplayTime > TimeSpan.Zero)
                {
                    await WaitForGameTime(remainingDisplayTime.Seconds, cancellationToken);
                }

            }
            catch (Exception e)
            {
                Debug.LogError($"Error displaying subtitle: {e.Message}");
            }
            finally
            {
                await transition.AnimateSubtitleExit(subtitle, entry, CancellationToken.None);

                activeSubtitleCount--;
                subtitlePool.Release(subtitle);
                UpdateSubtitlePanelVisibility();
            }
        }

        private void SetSubtitleText(Label subtitle, string speaker, string message)
        {
            string text = "";

            text = string.IsNullOrEmpty(speaker)
                ? message
                : $"<color=#{ColorUtility.ToHtmlStringRGB(speakerHighlight)}><b>{speaker}</b></color>:" + (lineBreakSpeaker ? "\n" : " ") + message;

            subtitle.text = text;
        }

        #region animations

        private void UpdateSubtitlePanelVisibility()
        {
            if (!enableBackgroundPanel) return;

            if (activeSubtitleCount == 0)
            {
                displayPanel.AddToClassList("SubtitlePanel_Hide");
                displayPanel.style.visibility = Visibility.Hidden;
            }
            else
            {
                displayPanel.RemoveFromClassList("SubtitlePanel_Hide");
                displayPanel.style.visibility = Visibility.Visible;
            }
        }

        #endregion

        private int GetNextSubtitleId()
        {
            return Interlocked.Increment(ref nextSubtitleId);
        }

        #region utils

        /// <summary>
        /// Waits for a specified amount of game time. Used by Subtitler (and optionally transition modules) to wait for a specific amount of time.
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="OperationCanceledException"></exception>
        static public async Task WaitForGameTime(float seconds, CancellationToken cancellationToken)
        {
            float startTime = Time.time;
            float targetTime = startTime + seconds;

            while (Time.time < targetTime)
            {
                await Task.Yield();
                if (cancellationToken.IsCancellationRequested)
                    throw new OperationCanceledException();
            }
        }

        #endregion
    }
}
