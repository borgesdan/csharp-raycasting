using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Raycasting
{
    public static class TextureHelper
    {
        public static Texture2D GetFilledTexture(Game game, int width, int height, Color color)
        {
            Color[] data;
            Texture2D texture;

            texture = new Texture2D(game.GraphicsDevice, width, height);            
            data = new Color[texture.Width * texture.Height];
            
            for (int i = 0; i < data.Length; ++i)
                data[i] = color;
            
            texture.SetData(data);

            return texture;
        }
    }
}
