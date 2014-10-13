using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoboWar
{
    public class Ev3Message
    {
        private Payload payload;

        /// <summary>
        /// The mailbox title of this message.
        /// </summary>
        public string MailboxTitle
        {
            get
            {
                return payload.Title;
            }
        }

        /// <summary>
        /// The value interpreted as text.
        /// </summary>
        public string ValueAsText
        {
            get
            {
                string value;
                ValueConverter.TryParse(payload.Value, out value);
                return value;
            }
        }

        /// <summary>
        /// The value interpreted as a number.
        /// The EV3 uses single precision floating point numbers internally.
        /// </summary>
        public float ValueAsNumber
        {
            get
            {
                float value;
                ValueConverter.TryParse(payload.Value, out value);
                return value;
            }
        }

        /// <summary>
        /// The value interpreted as logic value.
        /// </summary>
        public bool ValueAsLogic
        {
            get
            {
                bool value;
                ValueConverter.TryParse(payload.Value, out value);
                return value;
            }
        }

        /// <summary>
        /// Creates a message from a payload.
        /// (facade: hides complexity of the underlying protocol)
        /// </summary>
        /// <param name="payload"></param>
        internal Ev3Message(Payload payload)
        {
            this.payload = payload;
        }

        /// <summary>
        /// Returns a debug string.
        /// </summary>
        /// <returns>Returns a debug string.</returns>
        public override string ToString()
        {
            string textValue = ValueAsText;
            if (ContainsControlCharacters(textValue))
            {
                textValue = "";
            }

            return "Title: " + MailboxTitle + ", Text: " + textValue + ", Number: " + ValueAsNumber + ", Logic: " + ValueAsLogic;
        }

        private static bool ContainsControlCharacters(string text)
        {
            if (text != null)
            {
                foreach (char c in text)
                {
                    if (Char.IsControl(c))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
