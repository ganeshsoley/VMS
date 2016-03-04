using System;
using System.Collections;
using System.Diagnostics;

namespace EntityObject
{
    /// <summary>
    /// The BrokenRule class provides (localized) information about the broken rules of validators.
    /// </summary>
    public class BrokenRule
    {
        #region Public interface

        public BrokenRule()
        {
            // Broken business rules.
            // No duplicates allowed
            // so store rules by key.
            brokenRules = new Hashtable();
        }

        /// <summary>
        /// Is object in a valid state?
        /// i.e., are all business rules satisfied?
        /// </summary>
        public bool IsValid
        {
            get
            {
                return brokenRules.Count == 0;
            }
        }
        /// <summary>
        /// Diagnostics - lists broken rules.
        /// </summary>
        public void DisplayBrokenRules()
        {
            ICollection keys = brokenRules.Keys;

            foreach (string key in keys)
            {
                Debug.WriteLine(key);
            }
        }

        #endregion

        #region Events

        public delegate void EventHandler(object sender, EventArgs e);

        /// <summary>
        /// A business rule is broken.
        /// </summary>
        public event EventHandler OnInvalid;

        /// <summary>
        /// All business rules are satisfied.
        /// </summary>
        public event EventHandler OnValid;

        #endregion // Events

        #region Protected interface

        /// <summary>
        /// If rule is broken, adds it to collection of broken rules,
        /// otherwise removes it.
        /// </summary>
        /// <param name="rule">Description of the business rule.</param>
        /// <param name="isBroken">Is the rule broken?</param>
        protected void RuleBroken(string rule, bool isBroken)
        {
            if (isBroken)
            {
                AddBrokenRule(rule);
            }
            else
            {
                RemoveBrokenRule(rule);
            }
        }

        /// <summary>
        /// Common wrapper for raising simple events,
        /// i.e., where no information is supplied in EventArgs.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="e"></param>
        protected void RaiseEvent(EventHandler handler, EventArgs e)
        {
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        // Broken business rules
        protected Hashtable brokenRules;

        #endregion

        #region Implementation

        /// <summary>
        /// Adds broken rule to collection and notifies clients.
        /// </summary>
        /// <param name="rule">Description of the business rule.</param>
        private void AddBrokenRule(string rule)
        {
            bool isNewlyBroken =
                (brokenRules.ContainsKey(rule) == false);

            if (isNewlyBroken)
            {
                brokenRules.Add(rule, "");
                RaiseEvent(OnInvalid, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Removes broken rule from collection.
        /// Notifies clients when there are
        /// no more broken rules.
        /// </summary>
        /// <param name="rule">Description of the business rule.</param>
        private void RemoveBrokenRule(string rule)
        {
            brokenRules.Remove(rule);

            if (brokenRules.Count == 0)
            {
                RaiseEvent(OnValid, EventArgs.Empty);
            }
        }
        #endregion // Implementation

    }
}
