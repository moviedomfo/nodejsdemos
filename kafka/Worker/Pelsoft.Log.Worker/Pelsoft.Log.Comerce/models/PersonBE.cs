using Newtonsoft.Json;


namespace Pelsoft.Log.Comerce.BE
{
    public class PersonBE 

    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public DateTime? GeneratedDate { get; set; }

        public string kafka_Topic { get; set; }

        public string DocNumber { get; set; }
        [JsonIgnore]
        public string FullName
        {
            get
            {
                return FirstName + ", " + Lastname;
            }
        }





    

        public PersonBE Clone()
        {
            string json = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<PersonBE>(json);
        }
    }
}
