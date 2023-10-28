using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGPT
{
    public static class EventUtilities
    {
        // Define a custom event args class to pass data with events.
        public class CustomEventArgs : EventArgs
        {
            public string Message { get; }

            public CustomEventArgs(string message)
            {
                Message = message;
            }
        }

        // Define a custom event delegate.
        public delegate void CustomEventHandler(object sender, CustomEventArgs e);

        // An example of a custom event.
        public static event CustomEventHandler CustomEvent;

        // Method to trigger the custom event.
        public static void TriggerCustomEvent(string message)
        {
            CustomEvent?.Invoke(null, new CustomEventArgs(message));
        }

        // Method to log events (could be written to a file or console).
        public static void LogEvent(string message)
        {
            Console.WriteLine($"[{DateTime.Now}] {message}");
            // You can expand this to write to a file or other logging mechanisms.
        }

        // Method to subscribe to an event.
        public static void SubscribeToEvent(EventHandler eventHandler, Action<object, EventArgs> action)
        {
            eventHandler += new EventHandler(action);
        }

        // Method to unsubscribe from an event.
        public static void UnsubscribeFromEvent(EventHandler eventHandler, Action<object, EventArgs> action)
        {
            eventHandler -= new EventHandler(action);
        }

        // Method to batch subscribe to multiple events.
        public static void BatchSubscribeToEvents(Dictionary<EventHandler, Action<object, EventArgs>> eventActionPairs)
        {
            foreach (var pair in eventActionPairs)
            {
                EventHandler eventHandler = pair.Key;
                Action<object, EventArgs> action = pair.Value;
                eventHandler += new EventHandler(action);
            }
        }

        // Method to batch unsubscribe from multiple events.
        public static void BatchUnsubscribeFromEvents(Dictionary<EventHandler, Action<object, EventArgs>> eventActionPairs)
        {
            foreach (var pair in eventActionPairs)
            {
                EventHandler eventHandler = pair.Key;
                Action<object, EventArgs> action = pair.Value;
                eventHandler -= new EventHandler(action);
            }
        }
    }
}
