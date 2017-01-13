/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class SerializableData 
{
	// Adapted from http://stackoverflow.com/questions/1031023/copy-a-class-c-sharp
	public SerializableData Copy() {
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(memoryStream, this);
			memoryStream.Position = 0;
			return formatter.Deserialize(memoryStream) as SerializableData;
		}
	}
}
