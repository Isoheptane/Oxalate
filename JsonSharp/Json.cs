using System;
using System.Text;
using System.Collections.Generic;
namespace JsonSharp
{
    public enum ValueType
    {
        json = 0,
        array = 1,
        str = 2,
        integer = 3,
        number = 4,
        boolean = 5,
        nulltype = -1
    }
    // String Readers
    public static class Reader
    {
        public static bool IsSpace(char ch)
        {
            return ch <= 32;
        }
        public static void IgnoreSpaces(ref string str, ref int ptr)
        {
            while (ptr < str.Length && IsSpace(str[ptr]))
                ptr++;
        }
        public static string ReadString(ref string str, ref int ptr)
        {
            if (str[ptr] != '\"') throw new Exception("Expected \"\"\" at the head of a string definition.");
            StringBuilder ret = new StringBuilder();
            ptr++;
            while (str[ptr] != '\"')
            {
                if (str[ptr] == '\\')
                {
                    switch (str[ptr + 1])
                    {
                        case '\\': { ret.Append('\\'); break; }
                        case '\"': { ret.Append('\"'); break; }
                        case 'n': { ret.Append('\n'); break; }
                        case 'b': { ret.Append('\b'); break; }
                        case '0': { ret.Append('\0'); break; }
                        case 'a': { ret.Append('\a'); break; }
                        case 'f': { ret.Append('\f'); break; }
                        case 'r': { ret.Append('\r'); break; }
                        case 't': { ret.Append('\t'); break; }
                        case 'v': { ret.Append('\v'); break; }
                    }
                    ptr += 2;
                    continue;
                }
                ret.Append(str[ptr]);
                ptr++;
            }
            ptr++;
            return ret.ToString();
        }
        public static string ReadString(string str)
        {
            int ptr = 0;
            return ReadString(ref str, ref ptr);
        }
        public static string ReadToEnd(ref string str, ref int ptr)
        {
            string ret = "";
            while (
                ptr < str.Length &&
                str[ptr] != ',' &&
                str[ptr] != '}' &&
                str[ptr] != ']') ret += str[ptr++];
            return ret.Trim();
        }
        public static string Escape(string str)
        {
            string ret = "";
            foreach (char i in str)
            {
                switch (i)
                {
                    case '\\': { ret += "\\\\"; break; }
                    case '\"': { ret += "\\\""; break; }
                    case '\n': { ret += "\\n"; break; }
                    case '\b': { ret += "\\b"; break; }
                    case '\0': { ret += "\\0"; break; }
                    case '\a': { ret += "\\a"; break; }
                    case '\f': { ret += "\\f"; break; }
                    case '\r': { ret += "\\r"; break; }
                    case '\t': { ret += "\\t"; break; }
                    case '\v': { ret += "\\v"; break; }
                    default: { ret += i; break; }
                }
            }
            return ret;
        }
    }
    public class JsonValue
    {
        public ValueType type;
        object value;
        public JsonValue()
        {
            this.type = ValueType.nulltype;
            this.value = null;
        }
        public JsonValue(ValueType type, object value)
        {
            this.type = type;
            this.value = value;
        }
        public JsonValue this[string index]
        {
            get
            {
                if (type != ValueType.json) throw new Exception("Object is not a JSON object.");
                return ((JsonObject)this.value)[index];
            }
            set
            {
                if (type != ValueType.json) throw new Exception("Object is not a JSON object.");
                ((JsonObject)this.value)[index] = value;
            }
        }
        public JsonValue this[int index]
        {
            get
            {
                if (type != ValueType.array) throw new Exception("Object is not a array object.");
                return ((JsonArray)this.value)[index];
            }
            set
            {
                if (type != ValueType.array) throw new Exception("Object is not a array object.");
                ((JsonArray)this.value)[index] = value;
            }
        }
        public static implicit operator JsonValue(JsonObject val)
        {
            return new JsonValue(ValueType.json, val);
        }
        public static implicit operator JsonValue(JsonArray val)
        {
            return new JsonValue(ValueType.array, val);
        }
        public static implicit operator JsonValue(string val)
        {
            return new JsonValue(ValueType.str, val);
        }
        public static implicit operator JsonValue(UInt16 val)
        {
            return new JsonValue(ValueType.integer, Convert.ToInt64(val));
        }
        public static implicit operator JsonValue(UInt32 val)
        {
            return new JsonValue(ValueType.integer, Convert.ToInt64(val));
        }
        public static implicit operator JsonValue(UInt64 val)
        {
            return new JsonValue(ValueType.integer, Convert.ToInt64(val));
        }
        public static implicit operator JsonValue(Int16 val)
        {
            return new JsonValue(ValueType.integer, Convert.ToInt64(val));
        }
        public static implicit operator JsonValue(Int32 val)
        {
            return new JsonValue(ValueType.integer, Convert.ToInt64(val));
        }
        public static implicit operator JsonValue(Int64 val)
        {
            return new JsonValue(ValueType.integer, Convert.ToInt64(val));
        }
        public static implicit operator JsonValue(decimal val)
        {
            return new JsonValue(ValueType.number, val);
        }
        public static implicit operator JsonValue(bool val)
        {
            return new JsonValue(ValueType.boolean, val);
        }
        public static implicit operator JsonValue(DateTime val)
        {
            return new JsonValue(ValueType.str, val.ToString("yyyy/MM/dd HH:mm:ss"));
        }
        public static implicit operator string(JsonValue val)
        {
            if (val.type != ValueType.str)
                return val.ToString();
            else
                return val.value.ToString();
        }
        public static implicit operator UInt16(JsonValue val)
        {
            if (val.type != ValueType.integer && val.type != ValueType.number)
                throw new Exception("Object is not a number.");
            else
            {
                if (val.type == ValueType.integer) return Convert.ToUInt16(val.value);
                else return Convert.ToUInt16(val.value);
            }
        }
        public static implicit operator UInt32(JsonValue val)
        {
            if (val.type != ValueType.integer && val.type != ValueType.number)
                throw new Exception("Object is not a number.");
            else
            {
                if (val.type == ValueType.integer) return Convert.ToUInt32(val.value);
                else return Convert.ToUInt32(val.value);
            }
        }
        public static implicit operator UInt64(JsonValue val)
        {
            if (val.type != ValueType.integer && val.type != ValueType.number)
                throw new Exception("Object is not a number.");
            else
            {
                if (val.type == ValueType.integer) return Convert.ToUInt64(val.value);
                else return Convert.ToUInt64(val.value);
            }
        }
        public static implicit operator Int16(JsonValue val)
        {
            if (val.type != ValueType.integer && val.type != ValueType.number)
                throw new Exception("Object is not a number.");
            else
            {
                if (val.type == ValueType.integer) return Convert.ToInt16(val.value);
                else return Convert.ToInt16(val.value);
            }
        }
        public static implicit operator Int32(JsonValue val)
        {
            if (val.type != ValueType.integer && val.type != ValueType.number)
                throw new Exception("Object is not a number.");
            else
            {
                if (val.type == ValueType.integer) return Convert.ToInt32(val.value);
                else return Convert.ToInt32(val.value);
            }
        }
        public static implicit operator Int64(JsonValue val)
        {
            if (val.type != ValueType.integer && val.type != ValueType.number)
                throw new Exception("Object is not a number.");
            else
            {
                if (val.type == ValueType.integer) return Convert.ToInt64(val.value);
                else return Convert.ToInt64(val.value);
            }
        }
        public static implicit operator decimal(JsonValue val)
        {
            if (val.type != ValueType.integer && val.type != ValueType.number)
                throw new Exception("Object is not a number.");
            else
            {
                if (val.type == ValueType.integer) return Convert.ToDecimal(val.value);
                else return Convert.ToDecimal(val.value);
            }
        }
        public static implicit operator JsonObject(JsonValue val)
        {
            if (val.type != ValueType.json) throw new Exception("Object is not a JSON object");
            return (JsonObject)val.value;
        }
        public static implicit operator JsonArray(JsonValue val)
        {
            if (val.type != ValueType.array) throw new Exception("Object is not a array object");
            return (JsonArray)val.value;
        }
        public static implicit operator bool(JsonValue val)
        {
            if (val.type != ValueType.boolean) throw new Exception("Object is not a boolean object");
            return (bool)val.value;
        }
        public static implicit operator DateTime(JsonValue val)
        {
            if (val.type != ValueType.str)
                throw new Exception("Object is not a string value.");
            else
            {
                return DateTime.ParseExact((string)val, "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        public static JsonValue Parse(ref string json, ref int ptr)
        {
            if (json[ptr] == '\"')
                return new JsonValue(ValueType.str, Reader.ReadString(ref json, ref ptr));
            if (json[ptr] == '{')
                return new JsonValue(ValueType.json, JsonObject.Parse(ref json, ref ptr));
            if (json[ptr] == '[')
                return new JsonValue(ValueType.array, JsonArray.Parse(ref json, ref ptr));
            string content = Reader.ReadToEnd(ref json, ref ptr);
            if (content == "null")
                return new JsonValue(ValueType.nulltype, null);
            if (content == "true" || content == "false")
                return new JsonValue(ValueType.boolean, content == "true" ? true : false);
            Int64 retInt64;
            Decimal retDecimal;
            if (!content.Contains(".") && Int64.TryParse(content, out retInt64))
                return new JsonValue(ValueType.integer, retInt64);
            else if (decimal.TryParse(content, out retDecimal))
                return new JsonValue(ValueType.number, decimal.Parse(content));
            else
                return new JsonValue(ValueType.number, decimal.Parse(content, System.Globalization.NumberStyles.Float));
        }
        public static JsonValue Parse(string json)
        {
            int ptr = 0;
            return JsonValue.Parse(ref json, ref ptr);
        }
        public void RawToString(ref StringBuilder strb)
        {
            switch (type)
            {
                case ValueType.json: { ((JsonObject)value).RawToString(ref strb); break; }
                case ValueType.array: { ((JsonArray)value).RawToString(ref strb); break; }
                default:
                    {
                        switch (type)
                        {
                            case ValueType.nulltype: { strb.Append("null"); break; }
                            case ValueType.str: { strb.Append("\"" + Reader.Escape(value.ToString()) + "\""); break; }
                            case ValueType.boolean: { strb.Append((bool)value ? "true" : "false"); break; }
                            default: { strb.Append(value.ToString()); break; }
                        }
                        break;
                    };
            }
        }
        public override string ToString()
        {
            StringBuilder strb = new StringBuilder();
            this.RawToString(ref strb);
            return strb.ToString();
        }
        public void Serialize(ref StringBuilder strb, string currentTab = "", string tab = "    ")
        {
            switch (type)
            {
                case ValueType.json: { ((JsonObject)value).Serialize(ref strb, currentTab, tab); break; }
                case ValueType.array: { ((JsonArray)value).Serialize(ref strb, currentTab, tab); break; }
                default: { this.RawToString(ref strb); break; }
            }
        }
        public string Serialize(string currentTab = "", string tab = "    ")
        {
            StringBuilder strb = new StringBuilder();
            this.Serialize(ref strb, currentTab, tab);
            return strb.ToString();
        }
    }
    public class JsonArray
    {
        public List<JsonValue> elements;
        public JsonArray()
        {
            elements = new List<JsonValue>();
        }
        public JsonValue this[int index]
        {
            get { return elements[index]; }
            set { elements[index] = value; }
        }
        public static JsonArray Parse(ref string json, ref int ptr)
        {
            JsonArray ret = new JsonArray();
            Reader.IgnoreSpaces(ref json, ref ptr);
            if (json[ptr] != '[') throw new Exception("Expected \"[\" of a array definition.");
            ptr++;
            Reader.IgnoreSpaces(ref json, ref ptr);
            if (json[ptr] == ']') { ptr++; return ret; };
            while (ptr < json.Length)
            {
                Reader.IgnoreSpaces(ref json, ref ptr);
                JsonValue val = JsonValue.Parse(ref json, ref ptr);
                ret.elements.Add(val);
                Reader.IgnoreSpaces(ref json, ref ptr);
                if (json[ptr] == ',') ptr++;
                else if (json[ptr] == ']') break;
                else throw new Exception("Excepted \",\" or \"}\" after a Key-Value pair.");
            }
            ptr++;
            return ret;
        }
        public static JsonArray Parse(string json)
        {
            int ptr = 0;
            return JsonArray.Parse(ref json, ref ptr);
        }
        public void RawToString(ref StringBuilder strb)
        {
            strb.Append("[");
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].RawToString(ref strb);
                if (i != elements.Count - 1)
                    strb.Append(", ");
            }
            strb.Append("]");
        }
        public override string ToString()
        {
            StringBuilder strb = new StringBuilder();
            this.RawToString(ref strb);
            return strb.ToString();
        }
        public void Serialize(ref StringBuilder strb, string currentTab = "", string tab = "    ")
        {
            if (elements.Count == 0)
            {
                strb.Append("[]");
                return;
            }
            strb.Append("[\n");
            for (int i = 0; i < elements.Count; i++)
            {
                strb.Append(currentTab + tab);
                elements[i].Serialize(ref strb, currentTab + tab, tab);
                if (i != elements.Count - 1)
                    strb.Append(", ");
                strb.Append('\n');
            }
            strb.Append(currentTab + "]");
        }
        public string Serialize(string currentTab = "", string tab = "    ")
        {
            StringBuilder strb = new StringBuilder();
            this.Serialize(ref strb, currentTab, tab);
            return strb.ToString();
        }
    }
    public class JsonObject
    {
        public Dictionary<string, JsonValue> pairs;
        List<string> keys;
        public JsonObject()
        {
            pairs = new Dictionary<string, JsonValue>();
            keys = new List<string>();
        }
        public void Add(string key, JsonValue val)
        {
            pairs.Add(key, val);
            keys.Add(key);
        }
        public void Delete(string key)
        {
            pairs.Remove(key);
            keys.Remove(key);
        }
        public bool Exist(string key) => pairs.ContainsKey(key);
        public JsonValue this[string index]
        {
            get { return pairs[index]; }
            set
            {
                if (!pairs.ContainsKey(index))
                {
                    keys.Add(index);
                    pairs.Add(index, value);
                }
                else
                    pairs[index] = value;
            }
        }
        public static JsonObject Parse(ref string json, ref int ptr)
        {
            JsonObject ret = new JsonObject();
            Reader.IgnoreSpaces(ref json, ref ptr);
            if (json[ptr] != '{') throw new Exception("Expected \"{\" of a JSON object definition.");
            ptr++;
            Reader.IgnoreSpaces(ref json, ref ptr);
            if (json[ptr] == '}') { ptr++; return ret; }
            while (ptr < json.Length)
            {
                Reader.IgnoreSpaces(ref json, ref ptr);
                string key = Reader.ReadString(ref json, ref ptr);
                Reader.IgnoreSpaces(ref json, ref ptr);
                if (json[ptr] != ':') throw new Exception("Expected \":\" after key.");
                ptr++;
                Reader.IgnoreSpaces(ref json, ref ptr);
                JsonValue val = JsonValue.Parse(ref json, ref ptr);
                ret.Add(key, val);
                Reader.IgnoreSpaces(ref json, ref ptr);
                if (json[ptr] == ',') ptr++;
                else if (json[ptr] == '}') break;
                else throw new Exception("Excepted \",\" or \"}\" after a Key-Value pair.");
            }
            ptr++;
            return ret;
        }
        public static JsonObject Parse(string json)
        {
            int ptr = 0;
            return JsonObject.Parse(ref json, ref ptr);
        }
        public void RawToString(ref StringBuilder strb)
        {
            strb.Append("{");
            for (int i = 0; i < keys.Count; i++)
            {
                strb.Append("\"" + Reader.Escape(keys[i]) + "\": ");
                pairs[keys[i]].RawToString(ref strb);
                if (i != keys.Count - 1)
                    strb.Append(", ");
            }
            strb.Append("}");
        }
        public override string ToString()
        {
            StringBuilder strb = new StringBuilder();
            this.RawToString(ref strb);
            return strb.ToString();
        }
        public void Serialize(ref StringBuilder strb, string currentTab = "", string tab = "    ")
        {
            if (pairs.Count == 0 && keys.Count == 0)
            {
                strb.Append("{}");
                return;
            }
            strb.Append("{\n");
            for (int i = 0; i < keys.Count; i++)
            {
                strb.Append(currentTab + tab + "\"");
                strb.Append(Reader.Escape(keys[i]));
                strb.Append("\": ");
                pairs[keys[i]].Serialize(ref strb, currentTab + tab, tab);
                if (i != keys.Count - 1)
                    strb.Append(", ");
                strb.Append('\n');
            }
            strb.Append(currentTab + "}");
        }
        public string Serialize(string currentTab = "", string tab = "    ")
        {
            StringBuilder strb = new StringBuilder();
            this.Serialize(ref strb, currentTab, tab);
            return strb.ToString();
        }
    }
}
