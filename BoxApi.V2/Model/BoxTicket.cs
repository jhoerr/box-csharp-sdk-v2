namespace BoxApi.V2.Model
{
    internal class BoxTicket
    {
        /// <summary>
        ///     Status of the ticket request
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     The ticket, if a valid one was returned
        /// </summary>
        public string Ticket { get; set; }
    }
}