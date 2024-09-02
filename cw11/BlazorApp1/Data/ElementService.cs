using Microsoft.VisualBasic;
using System.Runtime.Serialization;

namespace BlazorApp1.Data
{
    public interface IElementService
    {
        public List<Element> GetElements();

        public Element? GetElementById(int id);
    }
    public class ElementService : IElementService
    {
        private List<Element> _elements;
        public ElementService() 
        {
            _elements = new List<Element>
            {
                new Element
                {
                    ID = 1,
                    AvatarURL = "https://e7.pngegg.com/pngimages/377/755/png-clipart-apple-logo-carplay-apple-heart-logo-thumbnail.png",
                    FirstName = "Adam",
                    LastName = "Andrzejewski", 
                    Birthdate = new DateTime(2002,6,21),
                    Studies = "Informatyka"
                   
                },
                new Element
                {
                    ID = 2,
                    AvatarURL = "https://www.pngmart.com/files/15/Happy-Emoji-PNG.png",
                    FirstName = "Katarzyna",
                    LastName = "Przerwa",
                    Birthdate =  new DateTime(2003,1,12),
                    Studies = "Ogrodnictwo"
                },
                new Element
                {
                    ID = 3,
                    AvatarURL = "https://freepngimg.com/download/anonymous/36006-5-anonymous.png",
                    FirstName = "Krystian",
                    LastName = "Michalweski",
                    Birthdate = new DateTime(2001,3,18),
                    Studies = "Psychologia"

                },
                new Element
                {
                    ID = 4,
                    AvatarURL = "https://www.freepnglogos.com/uploads/anime-face-png/cartoon-anime-faces-png-9.png",
                    FirstName = "Anna",
                    LastName = "Marek",
                    Birthdate = new DateTime(2000,1,30),
                    Studies = "Ornitologia"

                },
                new Element
                {
                    ID = 5,
                    AvatarURL = "https://pngimg.com/d/simpsons_PNG95.png",
                    FirstName = "Paweł",
                    LastName = "Tartanus",
                    Birthdate = new DateTime(2002,7,17),
                    Studies = "Informatyka"

                },
            };
        }

        public Element? GetElementById(int id)
        {
            return _elements.FirstOrDefault(e => e.ID == id);
        }

        public List<Element> GetElements()
        {
            return _elements;
        }
    }
}
