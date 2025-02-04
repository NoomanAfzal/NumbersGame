// dnSpy decompiler from Assembly-CSharp.dll class: CSVReader
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class CSVReader
{
	public static List<List<string>> ReadCSVData(string resPath, int startLine = 0)
	{
		TextAsset textAsset = Resources.Load(resPath, typeof(TextAsset)) as TextAsset;
		string @string = Encoding.UTF8.GetString(textAsset.bytes);
		string[] array = @string.Split(new char[]
		{
			"\n"[0]
		});
		List<List<string>> list = new List<List<string>>();
		for (int i = startLine; i < array.Length; i++)
		{
			if (array[i].Length > 0)
			{
				List<string> list2 = new List<string>();
				string[] array2 = array[i].Split(new char[]
				{
					'|'
				});
				foreach (string item in array2)
				{
					list2.Add(item);
				}
				list.Add(list2);
			}
		}
		return list;
	}
}
