//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Helper_CL.custom
{
	public static class _extensions
	{
		public static List<T> ToListof<T>(this DataTable dt)
		{
			const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
			var columnNames = dt.Columns.Cast<DataColumn>()
				.Select(c => c.ColumnName)
				.ToList();
			var objectProperties = typeof(T).GetProperties(flags);
			var targetList = dt.AsEnumerable().Select(dataRow =>
			{
				var instanceOfT = Activator.CreateInstance<T>();

				foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
				{
					if (dataRow?[properties.Name] != null) properties.SetValue(instanceOfT, dataRow?[properties.Name], null);
				}
				return instanceOfT;
			}).ToList();

			return targetList;
		}

		public static T ToSingleOrDefault<T>(this DataTable dt)
		{
			try
			{
				var instance = Activator.CreateInstance<T>();
				var obj_properties = typeof(T).GetProperties();

				var column_names = dt.Columns.Cast<DataColumn>()
					.Select(c => c.ColumnName)
					.ToList();
				DataRow? dr = dt.Rows.Count > 0 ? dt.Rows[0] : null;

				foreach (var properties in obj_properties.Where(properties => column_names.Contains(properties.Name) && dr?[properties.Name] != DBNull.Value))
				{
					if (dr?[properties.Name] != null) properties.SetValue(instance, dr?[properties.Name], null);
				}
				return instance;
			}
			catch (Exception ex)
			{
				throw new Exception("Error at model property binding, " + ex.Message);
			}
		}

		public static JObject ToJObject(this DataTable dt)
		{
			JObject obj = new JObject();
			obj["data"] = dt.ToJArray();
			return obj;
		}

		public static JObject ToJObject(this DataSet ds)
		{
			JObject obj = new JObject();
			obj["data"] = ds.ToJArray();
			return obj;
		}

		public static JArray ToJArray(this DataTable dt)
		{
			return JArray.FromObject(GetRowToDictionary(dt));
		}

		public static JArray ToJArray(this DataSet ds)
		{
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			foreach (DataTable table in ds.Tables)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add(table.TableName, GetRowToDictionary(table));
				list.Add(dictionary);
			}
			return JArray.FromObject(list);
		}

		public static JArray ToJArrayWithEmpty(this DataSet ds)
		{
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			foreach (DataTable table in ds.Tables)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add(table.TableName, GetRowToDictionary(table));
				if (table.Rows.Count == 0) {
					dictionary[table.TableName] = new List<Dictionary<string, object>> { GetEmptyRow(table) };
					dictionary["no_data"] = true;
				}
				
				list.Add(dictionary);
			}
			return JArray.FromObject(list);
		}

		public static JArray ToJArrayInLowerKeysCase(this DataTable dt)
		{
			return JArray.FromObject(GetRowToDictionaryInLowerKeys(dt));
		}

		public static JArray ToJArrayInLowerKeysCase(this DataSet ds)
		{
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			foreach (DataTable table in ds.Tables)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add(table.TableName, GetRowToDictionaryInLowerKeys(table));
				list.Add(dictionary);
			}

			return JArray.FromObject(list);
		}

        public static JObject ToJObjectInLowerKeysCase(this DataSet ds)
        {
			try
			{
				JObject obj = new JObject();
				List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
				foreach (DataTable table in ds.Tables)
				{
					//Dictionary<string, object> dictionary = new Dictionary<string, object>();
					//dictionary.Add(table.TableName, GetRowToDictionaryInLowerKeys(table));
					//list.Add(dictionary);
					obj[table.TableName.ToLower()] = JArray.FromObject(GetRowToDictionaryInLowerKeys(table));

                }

                //return JObject.FromObject(list);
                return obj;
            }
			catch (Exception ex){
				throw ex;
			}
            
        }

        public static string GetSP(this Enum value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());

			DescriptionAttribute[] attributes =
				(DescriptionAttribute[])fi.GetCustomAttributes(
				typeof(DescriptionAttribute),
				false);

			if (attributes != null &&
				attributes.Length > 0)
				return attributes[0].Description;
			else
				return value.ToString();
		}


		private static List<Dictionary<string, object>> GetRowToDictionaryInLowerKeys(DataTable dt)
		{
			List<Dictionary<string, object>> dataRows = new List<Dictionary<string, object>>();
			dt.Rows.Cast<DataRow>().ToList().ForEach(delegate (DataRow dataRow)
			{
				Dictionary<string, object> row = new Dictionary<string, object>();
				dt.Columns.Cast<DataColumn>().ToList().ForEach(delegate (DataColumn column)
				{
					row.Add(column.ColumnName.ToLower(), dataRow[column]);
				});
				dataRows.Add(row);
			});
			return dataRows;
		}

		private static List<Dictionary<string, object>> GetRowToDictionary(DataTable dt)
		{
			List<Dictionary<string, object>> dataRows = new List<Dictionary<string, object>>();
			dt.Rows.Cast<DataRow>().ToList().ForEach(delegate (DataRow dataRow)
			{
				Dictionary<string, object> row = new Dictionary<string, object>();
				dt.Columns.Cast<DataColumn>().ToList().ForEach(delegate (DataColumn column)
				{
					row.Add(column.ColumnName, dataRow[column]);
				});
				dataRows.Add(row);
			});
			return dataRows;
		}

		private static Dictionary<string, object> GetEmptyRow(DataTable dt)
		{
			List<Dictionary<string, object>> data_rows = new List<Dictionary<string, object>>();
			Dictionary<string, object> row = new Dictionary<string, object>();
			dt.Columns.Cast<DataColumn>().ToList().ForEach(delegate (DataColumn column)
			{
				row.Add(column.ColumnName, null);
			});
			data_rows.Add(row);
			return row;
		}


		public static string friendly_url(this string url, bool remap_to_ascii = false, int maxlength = 180)
		{
			if (url == null)
			{
				return string.Empty;
			}

			int length = url.Length;
			bool prevdash = false;
			StringBuilder stringBuilder = new StringBuilder(length);
			char c;

			for (int i = 0; i < length; ++i)
			{
				c = url[i];
				if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
				{
					stringBuilder.Append(c);
					prevdash = false;
				}
				else if (c >= 'A' && c <= 'Z')
				{
					// tricky way to convert to lower-case
					stringBuilder.Append((char)(c | 32));
					prevdash = false;
				}
				else if ((c == ' ') || (c == ',') || (c == '.') || (c == '/') ||
				  (c == '\\') || (c == '-') || (c == '_') || (c == '='))
				{
					if (!prevdash && (stringBuilder.Length > 0))
					{
						stringBuilder.Append('-');
						prevdash = true;
					}
				}
				else if (c >= 128)
				{
					int previousLength = stringBuilder.Length;

					if (remap_to_ascii)
					{
						stringBuilder.Append(remap_international_char_to_ascii(c));
					}
					else
					{
						stringBuilder.Append(c);
					}

					if (previousLength != stringBuilder.Length)
					{
						prevdash = false;
					}
				}

				if (i == maxlength)
				{
					break;
				}
			}

			if (prevdash)
			{
				return stringBuilder.ToString().Substring(0, stringBuilder.Length - 1);
			}
			else
			{
				return stringBuilder.ToString();
			}
		}

		/// <summary>
		/// Remaps the international character to their equivalent ASCII characters. See
		/// </summary>
		/// <param name="character">The character to remap to its ASCII equivalent.</param>
		/// <returns>The remapped character</returns>
		private static string remap_international_char_to_ascii(char character)
		{
			string s = character.ToString().ToLowerInvariant();
			if ("àåáâäãåąā".Contains(s))
			{
				return "a";
			}
			else if ("èéêëę".Contains(s))
			{
				return "e";
			}
			else if ("ìíîïı".Contains(s))
			{
				return "i";
			}
			else if ("òóôõöøő".Contains(s))
			{
				return "o";
			}
			else if ("ùúûüŭů".Contains(s))
			{
				return "u";
			}
			else if ("çćčĉ".Contains(s))
			{
				return "c";
			}
			else if ("żźž".Contains(s))
			{
				return "z";
			}
			else if ("śşšŝ".Contains(s))
			{
				return "s";
			}
			else if ("ñń".Contains(s))
			{
				return "n";
			}
			else if ("ýÿ".Contains(s))
			{
				return "y";
			}
			else if ("ğĝ".Contains(s))
			{
				return "g";
			}
			else if (character == 'ř')
			{
				return "r";
			}
			else if (character == 'ł')
			{
				return "l";
			}
			else if ("đð".Contains(s))
			{
				return "d";
			}
			else if (character == 'ß')
			{
				return "ss";
			}
			else if (character == 'Þ')
			{
				return "th";
			}
			else if (character == 'ĥ')
			{
				return "h";
			}
			else if (character == 'ĵ')
			{
				return "j";
			}
			else
			{
				return string.Empty;
			}
		}

	}
}
