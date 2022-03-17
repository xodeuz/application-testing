namespace Sample.Domain.Contacts.Services
{
    internal class PitfallService
    {
        /// <summary>
        ///     Example of a classical issue when writing tests
        /// </summary>
        /// <remarks>
        ///     Due to the use of datetime within this method writing tests
        ///     towards this method is difficult. 
        ///     
        ///     Options: Pass DateTime in as parameter or use an abstraction to retrieve datetimes
        /// </remarks>
        public string HardToTest()
        {
            if(DateTimeOffset.UtcNow.Hour > 18)
            {
                return "Night";
            }
            return "Day";
        }
    }
}
