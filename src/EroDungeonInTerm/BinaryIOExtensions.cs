using System;
using System.IO;
using System.Text;

namespace EroDungeonInTerm;

public static class BinaryIOExtensions
{
	public static unsafe void WriteUtf8(this BinaryWriter writer, string value)
	{
		ArgumentNullException.ThrowIfNull(value, nameof(value));
		ArgumentNullException.ThrowIfNull(writer, nameof(writer));

		int bytesCount = Encoding.UTF8.GetByteCount(value);

		Span<byte> buffer = stackalloc byte[bytesCount];

		writer.Write7BitEncodedInt(bytesCount);
		Encoding.UTF8.GetBytes(value, buffer);
		writer.Write(buffer);
	}

	public static unsafe string ReadUtf8(this BinaryReader reader)
	{
		ArgumentNullException.ThrowIfNull(reader, nameof(reader));

		int bytesCount = reader.Read7BitEncodedInt();

		Span<byte> bytes = stackalloc byte[bytesCount];
		reader.Read(bytes);
		return Encoding.UTF8.GetString(bytes);
	}
}