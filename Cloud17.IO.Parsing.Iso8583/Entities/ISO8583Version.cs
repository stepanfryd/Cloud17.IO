namespace Cloud17.IO.Parsing.Iso8583.Entities
{
	/// <summary>
	/// The first digit of the MTI indicates the ISO 8583 version in which the message is encoded.
	/// </summary>
	public enum ISO8583Version : byte
	{
		/// <summary>
		/// ISO 8583:1987
		/// </summary>
		ISO8583_1987 = 0,

		/// <summary>
		/// ISO 8583:1993
		/// </summary>
		ISO8583_1993 = 1,

		/// <summary>
		/// ISO 8583:2003
		/// </summary>
		ISO8583_2003 = 2,

		/// <summary>
		/// Reserved by ISO
		/// </summary>
		ReservedByISO_1 = 3,

		/// <summary>
		/// Reserved by ISO
		/// </summary>
		ReservedByISO_2 = 4,

		/// <summary>
		/// Reserved by ISO
		/// </summary>
		ReservedByISO_3 = 5,

		/// <summary>
		/// Reserved by ISO
		/// </summary>
		ReservedByISO_4 = 6,

		/// <summary>
		/// Reserved by ISO
		/// </summary>
		ReservedByISO_5 = 7,

		/// <summary>
		/// National use
		/// </summary>
		NationalUse = 8,

		/// <summary>
		/// Private use
		/// </summary>
		PrivateUse = 9
	}
}
