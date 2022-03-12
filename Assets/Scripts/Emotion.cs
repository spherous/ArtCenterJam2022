using UnityEngine;

namespace Emotions
{
    public enum Emotion {None = 0, Anger = 1, Fear = 2, Sadness = 3, Peace = 4, Joy = 5, Love = 6}

    public static class EmotionExtensions
    {
        public static Color GetColor(this Emotion emotion) => emotion switch {
            Emotion.Anger => Color.red,
            Emotion.Fear => new Color(106f/255f, 45f/255f, 204f/255f, 1f),
            Emotion.Sadness => Color.blue,
            Emotion.Peace => Color.cyan,
            Emotion.Joy => Color.yellow,
            Emotion.Love => Color.magenta,
            _ => Color.white
        };

        public static float GetSpeed(this Emotion emotion) => emotion switch {
            Emotion.Anger => 40f,
            Emotion.Fear => 60f,
            Emotion.Sadness => 20f,
            Emotion.Peace => -20f,
            Emotion.Joy => -60f,
            Emotion.Love => -40f,
            _ => 0f
        };

        public static Emotion GetRandomEmotion() => (Emotion)Random.Range(1, 7);
    }
}