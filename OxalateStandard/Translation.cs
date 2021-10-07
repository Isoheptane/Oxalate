using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Oxalate.Standard;
using JsonSharp;

namespace Oxalate.Standard
{
    public class Translation
    {
        Dictionary<string, string> dictionary;

        /// <summary>
        /// Create a empty Translation instance.
        /// </summary>
        public Translation()
        {
            dictionary = new Dictionary<string, string>();
        }

        /// <summary>
        /// Create a Translation instance with JSON lang file.
        /// </summary>
        /// <param name="langFile">JSON lang file</param>
        public Translation(JsonObject langFile)
        {
            dictionary = new Dictionary<string, string>();
            LoadLangFile(langFile);
        }

        public string this[string index]
        {
            get 
            {
                if (dictionary.ContainsKey(index))
                {
                    return dictionary[index];
                }
                else
                {
                    return index;
                }
            }
        }

        /// <summary>
        /// Add a new entry to the dictionary.
        /// </summary>
        public void AddNewEntry(string key, string translation)
        {
            dictionary.Add(key, translation);
        }

        /// <summary>
        /// Load translations from a JSON lang file.
        /// </summary>
        /// <param name="langFile">JSON lang file</param>
        public void LoadLangFile(JsonObject langFile)
        {
            foreach (var record in langFile.pairs)
            {
                dictionary.Add(record.Key, record.Value);
            }
        }
    }
}
