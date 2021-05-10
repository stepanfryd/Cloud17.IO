namespace Cloud17.IO.Parsing.Iso8583.Entities
{
	/// <summary>
	/// Position four of the MTI defines the location of the message source within the payment chain.
	/// </summary>
	public enum MessageOrigin : byte
	{
		Acquirer = 0,
		AcquirerRepeat = 1,
		Issuer = 2,
		IssuerRepeat = 3,
		Other = 4,
		OtherRepeat = 5,
		ReservedByISO = 6,
		ReservedByISO_2 = 7,
		ReservedByISO_3 = 8,
		ReservedByISO_4 = 9,
	}
}
