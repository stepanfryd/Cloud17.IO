namespace Cloud17.IO.Parsing.Iso8583.Entities
{
	/// <summary>
	/// Position three of the MTI specifies the message function which defines how the message 
	/// should flow within the system. Requests are end-to-end messages (e.g., from acquirer to 
	/// issuer and back with time-outs and automatic reversals in place), while advices are 
	/// point-to-point messages (e.g., from terminal to acquirer, from acquirer to network, 
	/// from network to issuer, with transmission guaranteed over each link, but not necessarily immediately).
	/// </summary>
	public enum MessageFunction : byte
	{
		// <summary>
		/// Request from acquirer to issuer to carry out an action; issuer may accept or reject
		/// </summary>
		Request = 0,

		/// <summary>
		/// Issuer response to a request
		/// </summary>
		RequestResponse = 1,

		/// <summary>
		/// Advice that an action has taken place; receiver can only accept, not reject
		/// </summary>
		Advice = 2,

		/// <summary>
		/// Response to an advice
		/// </summary>
		AdviceResponse = 3,

		/// <summary>
		/// Notification that an event has taken place; receiver can only accept, not reject
		/// </summary>
		Notification = 4,

		/// <summary>
		/// Response to a notification
		/// </summary>
		NotificationAcknowledgement = 5,

		/// <summary>
		/// ISO 8583:2003
		/// </summary>
		Instruction = 6,

		/// <summary>
		/// ISO 8583:2003
		/// </summary>
		InstructionAcknowledgement = 7,

		/// <summary>
		/// Some implementations (such as MasterCard) use for positive acknowledgment
		/// </summary>
		ReservedForISOUse_1 = 8,

		/// <summary>
		/// Some implementations (such as MasterCard) use for negative acknowledgement
		/// </summary>
		ReservedForISOUse_2 = 9
	}
}
