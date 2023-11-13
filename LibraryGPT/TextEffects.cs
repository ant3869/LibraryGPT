using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace LibraryGPT
{
    public static class TextEffects
    {
        /// <summary>
        /// Creates a typewriter effect for a label's text.
        /// </summary>
        /// <param name="label">The label to apply the effect to.</param>
        /// <param name="text">The text to display in typewriter style.</param>
        /// <param name="delay">Delay in milliseconds between each character.</param>
        /// <example>
        /// TypewriterEffect(yourLabel, "Hello, World!", 50);
        /// </example>
        public static async void TypewriterEffect(Label label, string text, int delay)
        {
            label.Text = "";
            foreach (var ch in text)
            {
                label.Text += ch;
                await Task.Delay(delay);
                Application.DoEvents(); // Keeps the UI responsive
            }
        }

        /// <summary>
        /// Starts a scrolling marquee effect on a label.
        /// </summary>
        /// <param name="label">The label to apply the marquee effect.</param>
        /// <param name="speed">Speed of the marquee in milliseconds.</param>
        /// <example>
        /// StartMarquee(yourLabel, 100);
        /// </example>
        public static void StartMarquee(Label label, int speed)
        {
            label.Text = "Your scrolling text here ";
            System.Windows.Forms.Timer timer = new() { Interval = speed };
            timer.Tick += (s, e) => { label.Text = label.Text[^1] + label.Text[..^1]; };
            timer.Start();
        }

        /// <summary>
        /// Appends text to a RichTextBox and colors specified keywords.
        /// </summary>
        /// <param name="box">The RichTextBox to append text to.</param>
        /// <param name="text">The text to append.</param>
        /// <param name="keywords">Array of keywords to colorize.</param>
        /// <param name="color">Color to apply to the keywords.</param>
        /// <example>
        /// AppendTextWithColor(yourRichTextBox, "New log entry: Success!", new string[] { "Error", "Success" }, Color.Green);
        /// </example>
        public static void AppendTextWithColor(RichTextBox box, string text, string[] keywords, Color color)
        {
            box.AppendText(text);
            foreach (var keyword in keywords)
            {
                int start = 0;
                while (start < box.TextLength && (start = box.Text.IndexOf(keyword, start)) != -1)
                {
                    box.Select(start, keyword.Length);
                    box.SelectionColor = color;
                    start += keyword.Length;
                }
            }
            box.SelectionStart = box.Text.Length;
            box.ScrollToCaret();
        }

        /// <summary>
        /// Makes the text of a label blink.
        /// </summary>
        /// <param name="label">The label to apply the blinking effect.</param>
        /// <param name="interval">Interval in milliseconds for the blink effect.</param>
        /// <example>
        /// StartBlinking(yourLabel, 500);
        /// </example>
        public static void StartBlinking(Label label, int interval)
        {
            bool isVisible = true;
            System.Windows.Forms.Timer blinkTimer = new() { Interval = interval };

            blinkTimer.Tick += (sender, args) =>
            {
                label.Visible = isVisible;
                isVisible = !isVisible;
            };

            blinkTimer.Start();
        }

        /// <summary>
        /// Creates a pulse effect on the text size of a label.
        /// </summary>
        /// <param name="label">The label to apply the pulse effect.</param>
        /// <param name="maxSize">Maximum font size during the pulse.</param>
        /// <param name="minSize">Minimum font size during the pulse.</param>
        /// <param name="speed">Speed of the pulse effect.</param>
        /// <example>
        /// StartTextPulse(yourLabel, 16, 12, 100);
        /// </example>
        public static void StartTextPulse(Label label, float maxSize, float minSize, int speed)
        {
            System.Windows.Forms.Timer pulseTimer = new();
            bool increasing = true;
            pulseTimer.Interval = speed;

            pulseTimer.Tick += (sender, args) =>
            {
                float currentSize = label.Font.Size;

                if (increasing)
                {
                    if (currentSize < maxSize)
                        label.Font = new Font(label.Font.FontFamily, currentSize + 0.5f, label.Font.Style);
                    else
                        increasing = false;
                }
                else
                {
                    if (currentSize > minSize)
                        label.Font = new Font(label.Font.FontFamily, currentSize - 0.5f, label.Font.Style);
                    else
                        increasing = true;
                }
            };

            pulseTimer.Start();
        }


        /// <summary>
        /// Gradually fades in the text of a label.
        /// </summary>
        /// <param name="label">The label to apply the fade-in effect.</param>
        /// <param name="duration">Duration in milliseconds for the full fade-in.</param>
        /// <example>
        /// FadeInText(yourLabel, 2000);
        /// </example>
        public static async void FadeInText(Label label, int duration)
        {
            float step = 0.05f; // Increment for opacity
            for (float opacity = 0; opacity <= 1; opacity += step)
            {
                label.ForeColor = Color.FromArgb((int)(opacity * 255), label.ForeColor);
                await Task.Delay(duration / (int)(1 / step));
            }
        }

        /// <summary>
        /// Creates a wave effect on a label's text.
        /// </summary>
        /// <param name="label">The label to apply the wave effect.</param>
        /// <param name="amplitude">The amplitude of the wave.</param>
        /// <param name="frequency">Frequency of the wave oscillations.</param>
        /// <example>
        /// StartTextWave(yourLabel, 5, 100);
        /// </example>
        public static void StartTextWave(Label label, int amplitude, int frequency)
        {
            int originalY = label.Location.Y;
            int wavePosition = 0;
            System.Windows.Forms.Timer waveTimer = new() { Interval = frequency };

            waveTimer.Tick += (sender, args) =>
            {
                label.Location = new Point(label.Location.X, originalY + (int)(amplitude * Math.Sin(wavePosition * Math.PI / 180)));
                wavePosition = (wavePosition + 1) % 360;
            };

            waveTimer.Start();
        }

        /// <summary>
        /// Creates a waveform effect on the text of a label.
        /// </summary>
        /// <param name="label">The label to apply the waveform effect.</param>
        /// <param name="amplitude">The amplitude of the wave.</param>
        /// <param name="frequency">Frequency of the wave oscillations.</param>
        /// <example>
        /// StartTextWaveform(yourLabel, 10, 50);
        /// </example>
        public static void StartTextWaveform(Label label, int amplitude, int frequency)
        {
            int originalX = label.Location.X;
            int wavePosition = 0;
            System.Windows.Forms.Timer waveTimer = new() { Interval = frequency };

            waveTimer.Tick += (sender, args) =>
            {
                label.Location = new Point(originalX + (int)(amplitude * Math.Sin(wavePosition * Math.PI / 180)), label.Location.Y);
                wavePosition = (wavePosition + 1) % 360;
            };

            waveTimer.Start();
        }

        /// <summary>
        /// Cycles through colors for the text of a label.
        /// </summary>
        /// <param name="label">The label to apply the color cycling effect.</param>
        /// <param name="colors">Array of colors to cycle through.</param>
        /// <param name="interval">Interval in milliseconds for color change.</param>
        /// <example>
        /// StartColorCycle(yourLabel, new Color[] { Color.Red, Color.Green, Color.Blue }, 500);
        /// </example>
        public static void StartColorCycle(Label label, Color[] colors, int interval)
        {
            int currentColorIndex = 0;
            System.Windows.Forms.Timer colorTimer = new System.Windows.Forms.Timer { Interval = interval };

            colorTimer.Tick += (sender, args) =>
            {
                label.ForeColor = colors[currentColorIndex];
                currentColorIndex = (currentColorIndex + 1) % colors.Length;
            };

            colorTimer.Start();
        }

        /// <summary>
        /// Creates a rainbow color effect on the text of a label.
        /// </summary>
        /// <param name="label">The label to apply the rainbow effect.</param>
        /// <param name="interval">Interval in milliseconds for color change.</param>
        /// <example>
        /// StartTextRainbow(yourLabel, 200);
        /// </example>
        public static void StartTextRainbow(Label label, int interval)
        {
            int hue = 0;
            System.Windows.Forms.Timer rainbowTimer = new() { Interval = interval };

            rainbowTimer.Tick += (sender, args) =>
            {
                label.ForeColor = ColorFromHSL(hue, 0.5, 0.5);
                hue = (hue + 1) % 360;
            };

            rainbowTimer.Start();
        }

        private static Color ColorFromHSL(int hue, double saturation, double lightness)
        {
            double r;
            double g;
            double b;

            if (saturation == 0)
                r = g = b = lightness; // Achromatic color (gray)
            else
            {
                var q = lightness < 0.5 ? lightness * (1 + saturation) : lightness + saturation - lightness * saturation;
                var p = 2 * lightness - q;
                r = HueToRGB(p, q, hue / 360d + 1d / 3d);
                g = HueToRGB(p, q, hue / 360d);
                b = HueToRGB(p, q, hue / 360d - 1d / 3d);
            }

            return Color.FromArgb(255, (int)(r * 255), (int)(g * 255), (int)(b * 255));
        }

        private static double HueToRGB(double p, double q, double t)
        {
            if (t < 0) t += 1;
            if (t > 1) t -= 1;
            if (t < 1d / 6d) return p + (q - p) * 6 * t;
            if (t < 1d / 2d) return q;
            if (t < 2d / 3d) return p + (q - p) * (2d / 3d - t) * 6;

            return p;
        }

        /// <summary>
        /// Adds a shadow effect to the text of a label.
        /// </summary>
        /// <param name="label">The label to apply the shadow effect.</param>
        /// <param name="shadowColor">Color of the shadow.</param>
        /// <param name="offset">Offset of the shadow from the text.</param>
        /// <example>
        /// AddTextShadow(yourLabel, Color.Gray, new Point(2, 2));
        /// </example>
        public static void AddTextShadow(Label label, Color shadowColor, Point offset)
        {
            Label shadowLabel = new()
            {
                Text = label.Text,
                Font = label.Font,
                ForeColor = shadowColor,
                BackColor = Color.Transparent,
                Location = new Point(label.Location.X + offset.X, label.Location.Y + offset.Y),
                Size = label.Size
            };

            label.Parent.Controls.Add(shadowLabel);
            label.Parent.Controls.SetChildIndex(label, 0); // Bring the original label to front
        }

        /// <summary>
        /// Adds an outline effect to the text of a label.
        /// </summary>
        /// <param name="label">The label to apply the outline effect.</param>
        /// <param name="outlineColor">Color of the outline.</param>
        /// <param name="thickness">Thickness of the outline.</param>
        /// <example>
        /// AddTextOutline(yourLabel, Color.Black, 2);
        /// </example>
        public static void AddTextOutline(Label label, Color outlineColor, int thickness)
        {
            // This is a simplistic approach and might not be perfect.
            // For more accuracy, custom drawing on the label's Paint event would be required.
            for (int x = -thickness; x <= thickness; x++)
            {
                for (int y = -thickness; y <= thickness; y++)
                {
                    if (x != 0 || y != 0)
                    {
                        Label outlineLabel = new()
                        {
                            Text = label.Text,
                            Font = label.Font,
                            ForeColor = outlineColor,
                            BackColor = Color.Transparent,
                            Location = new Point(label.Location.X + x, label.Location.Y + y),
                            Size = label.Size
                        };
                        label.Parent.Controls.Add(outlineLabel);
                        label.Parent.Controls.SetChildIndex(outlineLabel, label.Parent.Controls.GetChildIndex(label));
                    }
                }
            }
            label.Parent.Controls.SetChildIndex(label, 0); // Bring the original label to front
        }

        /// <summary>
        /// Applies a gradient color effect to the text of a label.
        /// </summary>
        /// <param name="label">The label to apply the gradient effect.</param>
        /// <param name="startColor">Start color of the gradient.</param>
        /// <param name="endColor">End color of the gradient.</param>
        /// <example>
        /// ApplyTextGradient(yourLabel, Color.Blue, Color.Red);
        /// </example>
        public static void ApplyTextGradient(Label label, Color startColor, Color endColor)
        {
            label.Paint += (sender, e) =>
            {
                var brush = new System.Drawing.Drawing2D.LinearGradientBrush(label.ClientRectangle, startColor, endColor, 0f);
                e.Graphics.FillRectangle(brush, label.ClientRectangle);
                brush.Dispose();
            };
        }


        /// <summary>
        /// Applies a zoom-in effect to the text of a label on mouse hover.
        /// </summary>
        /// <param name="label">The label to apply the zoom effect.</param>
        /// <param name="zoomFactor">Factor by which to zoom the text.</param>
        /// <example>
        /// ApplyZoomEffect(yourLabel, 1.5f); // 1.5 times the original size
        /// </example>
        public static void ApplyZoomEffect(Label label, float zoomFactor)
        {
            Font originalFont = label.Font;

            label.MouseEnter += (sender, args) =>
            {
                label.Font = new Font(originalFont.FontFamily, originalFont.Size * zoomFactor, originalFont.Style);
            };

            label.MouseLeave += (sender, args) => { label.Font = originalFont; };
        }

        /// <summary>
        /// Expands and then contracts the text size of a label.
        /// </summary>
        /// <param name="label">The label to apply the expand effect.</param>
        /// <param name="maxSize">Maximum size to expand to.</param>
        /// <param name="speed">Speed of expansion and contraction.</param>
        /// <example>
        /// StartTextExpand(yourLabel, 20f, 100);
        /// </example>
        public static async void StartTextExpand(Label label, float maxSize, int speed)
        {
            float originalSize = label.Font.Size;

            for (float size = originalSize; size <= maxSize; size += 0.5f)
            {
                label.Font = new Font(label.Font.FontFamily, size, label.Font.Style);
                await Task.Delay(speed);
            }

            for (float size = maxSize; size >= originalSize; size -= 0.5f)
            {
                label.Font = new Font(label.Font.FontFamily, size, label.Font.Style);
                await Task.Delay(speed);
            }
        }

        /// <summary>
        /// Creates a text reveal effect on a label, character by character.
        /// </summary>
        /// <param name="label">The label to apply the text reveal effect.</param>
        /// <param name="textToReveal">Text to be revealed.</param>
        /// <param name="interval">Interval in milliseconds between each character reveal.</param>
        /// <example>
        /// StartTextReveal(yourLabel, "Reveal this text", 100);
        /// </example>
        public static async void StartTextReveal(Label label, string textToReveal, int interval)
        {
            label.Text = "";

            foreach (char c in textToReveal)
            {
                label.Text += c;
                label.ForeColor = Color.FromArgb(0, label.ForeColor);

                for (int i = 0; i <= 255; i += 5)
                {
                    label.ForeColor = Color.FromArgb(i, label.ForeColor);
                    await Task.Delay(interval / 51); // 51 is 255/5, for a smooth transition
                }
            }
        }

        /// <summary>
        /// Applies a shaking effect to the text of a label.
        /// </summary>
        /// <param name="label">The label to apply the shaking effect.</param>
        /// <param name="intensity">Intensity of the shake.</param>
        /// <param name="duration">Duration in milliseconds for the shaking effect.</param>
        /// <example>
        /// StartTextShake(yourLabel, 5, 1000);
        /// </example>
        public static void StartTextShake(Label label, int intensity, int duration)
        {
            Point originalLocation = label.Location;
            Random rnd = new();
            Timer shakeTimer = new() { Interval = 50 };

            shakeTimer.Tick += (sender, args) =>
            {
                label.Location = new Point(
                    originalLocation.X + rnd.Next(-intensity, intensity + 1),
                    originalLocation.Y + rnd.Next(-intensity, intensity + 1)
                );
            };

            shakeTimer.Start();

            Task.Delay(duration).ContinueWith(t =>
            {
                shakeTimer.Stop();
                label.Location = originalLocation;
            });
        }

        /// <summary>
        /// Applies a bouncing effect to the text of a label.
        /// </summary>
        /// <param name="label">The label to apply the bounce effect.</param>
        /// <param name="height">Height of the bounce.</param>
        /// <param name="speed">Speed of the bounce.</param>
        /// <example>
        /// StartTextBounce(yourLabel, 20, 200);
        /// </example>
        public static void StartTextBounce(Label label, int height, int speed)
        {
            int originalY = label.Location.Y;
            bool goingUp = true;
            Timer bounceTimer = new() {  Interval = speed };

            bounceTimer.Tick += (sender, args) =>
            {
                if (goingUp)
                {
                    if (label.Location.Y > originalY - height)
                        label.Location = new Point(label.Location.X, label.Location.Y - 1);
                    else
                        goingUp = false;
                }
                else
                {
                    if (label.Location.Y < originalY)
                        label.Location = new Point(label.Location.X, label.Location.Y + 1);
                    else
                        goingUp = true;
                }
            };

            bounceTimer.Start();
        }

        /// <summary>
        /// Creates a blur effect on the text of a label.
        /// </summary>
        /// <param name="label">The label to apply the blur effect.</param>
        /// <param name="duration">Duration in milliseconds for the blur effect.</param>
        /// <example>
        /// StartTextBlur(yourLabel, 1000);
        /// </example>
        public static async void StartTextBlur(Label label, int duration)
        {
            // Note: Real blurring requires custom drawing or external libraries
            // This is a simplified version using font size for a pseudo-blur effect
            float originalSize = label.Font.Size;
            float maxSize = originalSize + 5;

            for (float size = originalSize; size <= maxSize; size += 0.1f)
            {
                label.Font = new Font(label.Font.FontFamily, size, label.Font.Style);
                await Task.Delay(duration / 50); // Incrementally increase font size
            }

            for (float size = maxSize; size >= originalSize; size -= 0.1f)
            {
                label.Font = new Font(label.Font.FontFamily, size, label.Font.Style);
                await Task.Delay(duration / 50); // Incrementally decrease font size
            }
        }

        /// <summary>
        /// Creates a glitch effect on the text of a label.
        /// </summary>
        /// <param name="label">The label to apply the glitch effect.</param>
        /// <param name="interval">Interval in milliseconds for the glitch effect.</param>
        /// <param name="duration">Duration in milliseconds for each glitch occurrence.</param>
        /// <example>
        /// StartTextGlitch(yourLabel, 1000, 100);
        /// </example>
        public static void StartTextGlitch(Label label, int interval, int duration)
        {
            string originalText = label.Text;
            System.Windows.Forms.Timer glitchTimer = new() { Interval = interval };

            glitchTimer.Tick += (sender, args) =>
            {
                label.Text = new string(originalText.Select(c => c != ' ' && new Random().Next(2) == 0 ? (char)new Random().Next(32, 127) : c).ToArray());
                Task.Delay(duration).ContinueWith(t => label.Invoke(new Action(() => label.Text = originalText)));
            };

            glitchTimer.Start();
        }

        /// <summary>
        /// Creates a flash effect on the text of a label.
        /// </summary>
        /// <param name="label">The label to apply the flash effect.</param>
        /// <param name="interval">Interval in milliseconds for the flash effect.</param>
        /// <example>
        /// StartTextFlash(yourLabel, 300);
        /// </example>
        public static void StartTextFlash(Label label, int interval)
        {
            bool visible = true;
            Timer flashTimer = new() { Interval = interval };

            flashTimer.Tick += (sender, args) =>
            {
                label.Visible = visible;
                visible = !visible;
            };

            flashTimer.Start();
        }


        /// <summary>
        /// Creates an animated loading message with ellipsis on a label.
        /// </summary>
        /// <param name="label">The label to display the loading message.</param>
        /// <param name="message">The base message to display before the ellipsis.</param>
        /// <param name="interval">Interval in milliseconds for the ellipsis animation.</param>
        /// <example>
        /// StartLoadingMessage(yourLabel, "Loading assets", 500);
        /// </example>
        public static void StartLoadingMessage(Label label, string message, int interval)
        {
            int dotCount = 0;
            Timer loadingTimer = new()  { Interval = interval };

            loadingTimer.Tick += (sender, args) =>
            {
                dotCount = (dotCount + 1) % 4;
                string dots = new('.', dotCount);
                label.Text = $"{message}{dots}";
            };

            loadingTimer.Start();
        }

    }
}
