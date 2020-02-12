using System;
using System.Collections.Generic;
using System.Text;

namespace AireLogicBackEnd
{
    public class Recordings
    {
        public string Id { get; set; }
        public bool Video { get; set; }
        public int? Length { get; set; }
        public string Title { get; set; }
        public string Disambiguation { get; set; }
    }
}
