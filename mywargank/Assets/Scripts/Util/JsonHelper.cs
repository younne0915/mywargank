#if client
using UnityEngine;
#endif
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace Util
{
    public static class JsonHelper
    {
        static bool _Init = false;
        public static void Init()
        {
            if (!_Init)
            {
				#if client
                JsonMapper.RegisterExporter<Vector3>(ExportVector3);
                JsonMapper.RegisterExporter<Vector2>(ExportVector2);
                JsonMapper.RegisterExporter<Quaternion>(ExportQuaternion);
                JsonMapper.RegisterExporter<Rect>(ExportRect);
                JsonMapper.RegisterExporter<Color>(ExportColor);
				#endif
                JsonMapper.RegisterImporter<int, FixedNum>(ImportFixedNum);
                //	JsonMapper.RegisterExporter<GameObject>(ExportGameobject);
                _Init = true;
            }
        }

        public static FixedNum ImportFixedNum(int input)
        {
            return new FixedNum(input, FixedNumRatio.Permill);
        }

		#if client
        public static Hashtable LitJsonDecode(string jsonStr)
        {
            LitJson.JsonData litJsData = LitJson.JsonMapper.ToObject(jsonStr);
            return JsonDataToHashtable(litJsData);
        }



        private static Hashtable JsonDataToHashtable(JsonData data)
        {
            Hashtable ht = new Hashtable();
            if (data.Keys.Count > 0)
            {
                foreach (string k in data.Keys)
                {
                    if (data[k] == null)
                        continue;
                    if (data[k].IsObject)
                    {
                        ht.Add(k, JsonDataToHashtable(data[k]));
                    }
                    else if (data[k].IsArray)
                    {
                        ht.Add(k, JsonDataToList(data[k]));
                    }
                    else
                    {
                        ht.Add(k, data[k]);
                    }
                }
            }
            return ht;
        }

        private static ArrayList JsonDataToList(JsonData data)
        {
            ArrayList al = new ArrayList();
            if (data.Count > 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i] == null)
                    {
                        continue;
                    }
                    if (data[i].IsObject)
                    {
                        al.Add(JsonDataToHashtable(data[i]));
                    }
                    else if (data[i].IsArray)
                    {
                        al.Add(JsonDataToList(data[i]));
                    }
                    else
                    {
                        al.Add(ConvertHelper.ConvertToString(data[i]));
                    }
                }
            }
            return al;
        }

        public static void ExportVector2(Vector2 obj, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WritePropertyName("x");
            writer.Write(obj.x);
            writer.WritePropertyName("y");
            writer.Write(obj.y);
            writer.WriteObjectEnd();
        }

        public static void ExportRect(Rect obj, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WritePropertyName("x");
            writer.Write(obj.x);
            writer.WritePropertyName("y");
            writer.Write(obj.y);
            writer.WritePropertyName("width");
            writer.Write(obj.width);
            writer.WritePropertyName("height");
            writer.Write(obj.height);
            writer.WriteObjectEnd();
        }

        public static void ExportColor(Color color, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WritePropertyName("r");
            writer.Write(color.r);
            writer.WritePropertyName("g");
            writer.Write(color.g);
            writer.WritePropertyName("b");
            writer.Write(color.b);
            writer.WritePropertyName("a");
            writer.Write(color.a);
            writer.WriteObjectEnd();
        }

        public static void ExportVector3(Vector3 obj, JsonWriter writer)
        {
            //writer.Write(string.Format("{{ \"x\":{0},\"y\":{1},\"z\":{2} }}",obj.x,obj.y,obj.z));  
            writer.WriteObjectStart();
            writer.WritePropertyName("x");
            writer.Write(obj.x);
            //writer.Write(obj.x.ToString("F5"));   
            writer.WritePropertyName("y");
            writer.Write(obj.y);
            //writer.Write(obj.y.ToString("F5"));   
            writer.WritePropertyName("z");
            writer.Write(obj.z);
            //writer.Write(obj.z.ToString("F5"));   
            writer.WriteObjectEnd();
        }

        public static void ExportQuaternion(Quaternion obj, JsonWriter writer)
        {
            //writer.Write(string.Format("{{ \"x\":{0},\"y\":{1},\"z\":{2},\"w\":{3} }}",obj.x,obj.y,obj.z,obj.w));
            writer.WriteObjectStart();
            writer.WritePropertyName("x");
            writer.Write(obj.x);
            //writer.Write(obj.x.ToString("F5"));   
            writer.WritePropertyName("y");
            writer.Write(obj.y);
            //writer.Write(obj.y.ToString("F5"));   
            writer.WritePropertyName("z");
            writer.Write(obj.z);
            writer.WritePropertyName("w");
            writer.Write(obj.w);
            //writer.Write(obj.z.ToString("F5"));   
            writer.WriteObjectEnd();
        }
		#endif
    }
}
