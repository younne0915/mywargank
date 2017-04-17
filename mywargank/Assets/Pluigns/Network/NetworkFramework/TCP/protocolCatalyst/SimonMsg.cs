namespace Protocol
{
    using LitJson;
    using System;
    using System.Reflection;

    public class SimonMsg
    {
        public JsonData GetJD()
        {
            if (EncryptionConfig.USE_ENCRYPT_REQUEST)
            {
                Type type = this.GetType();
                string classFullName = type.FullName;
                if (EncryptionConfig.ENCRYPT_INFO.ContainsKey(classFullName))
                {
                    string[] enfieldNames = EncryptionConfig.ENCRYPT_INFO[classFullName];
                    foreach (string enfiledName in enfieldNames)
                    {
                        FieldInfo currentField = type.GetField(enfiledName);
                        string originalValue = currentField.GetValue(this).ToString();
                        string newValue = AES128Helper.Encrypt(originalValue, EncryptionConfig.GUESS_KEY);
                        currentField.SetValue(this, newValue);
                    }
                }
            }

            string jstr = JsonMapper.ToJson(this);
            JsonData jd = JsonMapper.ToObject(jstr);
            return jd;
        }
    }
}