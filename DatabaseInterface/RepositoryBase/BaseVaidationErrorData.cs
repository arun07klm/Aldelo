using System.Collections.Generic;

namespace DatabaseInterface
{
    public class BaseVaidationErrorData
    {
        public BaseVaidationErrorData(string typeName)
        {
            identifier = typeName;
        }

        private string identifier = string.Empty;

        private readonly Dictionary<string, string> m_validationErrors = new Dictionary<string, string>();
        public string Error
        {
            get
            {
                if (m_validationErrors.Count > 0)
                {
                    return identifier + " data is invalid";
                }
                else
                {
                    return null;
                }
            }
        }
        public string this[string columnName]
        {
            get
            {
                if (m_validationErrors.ContainsKey(columnName))
                {
                    return m_validationErrors[columnName];
                }
                else
                {
                    return null;
                }
            }
        }

        public void AddError(string columnName, string msg)
        {
            if (!m_validationErrors.ContainsKey(columnName))
            {
                m_validationErrors.Add(columnName, msg);
            }
        }

        public void RemoveError(string columnName)
        {
            if (m_validationErrors.ContainsKey(columnName))
            {
                m_validationErrors.Remove(columnName);
            }
        }
    }
}