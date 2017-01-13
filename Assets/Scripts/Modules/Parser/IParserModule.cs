/*
 * Author(s): Isaiah Mann
 * Desc: Parses json into C# Classes
 */

public interface IParserModule {

	T ParseJSONFromResources<T> (string fileName);
	T ParseJSON<T> (string json);

}
