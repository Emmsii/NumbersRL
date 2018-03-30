using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersRL
{
    public class Font
    {
        private readonly Texture2D _texture;
        private readonly int _characterWidth;
        private readonly int _characterHeight;
        private readonly String _chars;

        private readonly int _widthInCharacters;
        private readonly int _heightInCharacters;

        private Rectangle _destination = new Rectangle();
        private Rectangle source;

        private readonly Rectangle[,] _sourceRectangles;

        public Font(Texture2D texture, int characterWidth, int characterHeight, String chars)
        {
            _texture = texture;
            _characterWidth = characterWidth;
            _characterHeight = characterHeight;
            _chars = chars;
            _widthInCharacters = _texture.Width / _characterWidth;
            _heightInCharacters = _texture.Height / _characterHeight;

            

            Console.WriteLine(_widthInCharacters + "," + _heightInCharacters);

            _sourceRectangles = new Rectangle[_widthInCharacters, _heightInCharacters];

            CreateSourceRectangles();
        }

        private void CreateSourceRectangles()
        {
            for(int y = 0; y < _heightInCharacters; y++)
            {
                for(int x = 0; x < _widthInCharacters; x++)
                {
                    _sourceRectangles[x,y] = new Rectangle(x * _characterWidth, y * _characterHeight, _characterWidth, _characterHeight);
                }
            }
        }

        private Rectangle GetSourceRectangle(char c)
        {
            int index = _chars.IndexOf(c);
            if (index < 0) throw new ArgumentException("Invalid character '" + c + "'");
            return GetSourceRectangle(index % 16, index / 16);
        }

        private Rectangle GetSourceRectangle(int x, int y)
        {
            //if (x < 0 || y < 0 || x >= _widthInCharacters || y >= _heightInCharacters) throw new ArgumentOutOfRangeException(x + ", " + y + " is out of bounds of " + _widthInCharacters + ", " + _heightInCharacters);
            return _sourceRectangles[x, y];
        }

        public void DrawString(String text, int x, int y, int scale, Color color, SpriteBatch batch)
        {
            if (string.IsNullOrEmpty(text)) return;
            
            for (int i = 0; i < text.Length; i++)
            {
                source = GetSourceRectangle(text[i]);
                _destination.X = x * _characterWidth * scale + (i * _characterWidth) * scale;
                _destination.Y = y * _characterHeight * scale;
                _destination.Width = _characterWidth * scale;
                _destination.Height = _characterHeight * scale;

                batch.Draw(_texture, _destination, source, color);
            }
            
        }
    }
}
