using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{

    public class FaceBookData
    {
        public TaggableFriends Taggable_Friends { get; set; }
    }

    public class TaggableFriends
    {
        public List<FaceBookUser> Data { get; set; }
    }

    public class FaceBookUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Picture Picture { get; set; }
        public string PictureUrl
        {
            get
            {
                return this.Picture.Data.Url;
            }
        }
    }

    public class Picture
    {
        public FaceBookUserPicture Data { get; set; }
    }

    public class FaceBookUserPicture
    {
        public string Url { get; set; }
    }

}
