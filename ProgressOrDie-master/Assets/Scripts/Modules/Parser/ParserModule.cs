/*
 * Author(s): Isaiah Mann
 * Desc:
 */

using UnityEngine;

public class ParserModule : Module, IParserModule {

	public T ParseJSONFromResources<T> (string fileName) {
		string json = getTextAssetInResources(jsonPath(fileName)).text;
		return ParseJSON<T>(json);
	}

	public T ParseJSON<T> (string json) {
		return JsonUtility.FromJson<T>(json);
	}

	public string[,] ParseCSVFromResources (string fileName) {
		string csv = getTextAssetInResources(csvPath(fileName)).text;
		return ParseCSV(csv);
	}

	public string[,] ParseCSV (string csv) {
		string[,] result;
		string[] allStringsByLine = csv.Split('\n');
		string[][] allStringsByWord = new string[allStringsByLine.Length][];
		for (int i = 0; i < allStringsByLine.Length; i++) {
			allStringsByWord[i] = allStringsByLine[i].Split(',');
		}
		int width = allStringsByLine[0].Split(',').Length;
		int height = allStringsByLine.Length;
		result = new string[width, height];
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				result[x, y] = allStringsByWord[y][x].Trim();
			}
		}
		return result;
	}

	TextAsset getTextAssetInResources (string path) {
		return Resources.Load<TextAsset>(path);
	}

}
