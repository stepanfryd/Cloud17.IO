namespace Cloud17.IO.Parsing.Iso8583.Entities
{
	/// <summary>
	/// Position two of the MTI specifies the overall purpose of the message.
	/// </summary>
	public enum MessageClass : byte
	{
		/// <summary>
		/// Reserved by ISO
		/// </summary>
		ReservedByISO = 0,

		/// <summary>
		/// Determine if funds are available, get an approval but do not post to account for reconciliation. Dual message system (DMS), awaits file exchange for posting to the account.
		/// </summary>
		AuthorizationMessage = 1,

		/// <summary>
		/// Determine if funds are available, get an approval and post directly to the account. Single message system (SMS), no file exchange after this.
		/// </summary>
		FinancialMessages = 2,

		/// <summary>
		/// Used for hot-card, TMS and other exchanges
		/// </summary>
		FileActionsMessage = 3,

		/// <summary>
		/// Reversal (x4x0 or x4x1): Reverses the action of a previous authorization.Chargeback(x4x2 or x4x3): Charges back a previously cleared financial message.
		/// </summary>
		ReversalAndChargebackMessages = 4,

		/// <summary>
		/// Transmits settlement information message.
		/// </summary>
		ReconciliationMessage = 5,

		/// <summary>
		/// Transmits administrative advice. Often used for failure messages (e.g., message reject or failure to apply).
		/// </summary>
		AdministrativeMessage = 6,

		/// <summary>
		/// Fee collection messages
		/// </summary>
		FeeCollectionMessages = 7,

		/// <summary>
		/// Used for secure key exchange, logon, echo test and other network functions.
		/// </summary>
		NetworkManagementMessage = 8,

		/// <summary>
		/// Reserved by ISO
		/// </summary>
		ReservedByISO_2 = 9
	}
}
