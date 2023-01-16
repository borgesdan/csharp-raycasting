using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Raycasting
{
    public class Raycaster
    {
        protected Point mapSize;
        protected Viewport viewport;
        protected Vector2 position;
        protected Vector2 direction;
        protected Vector2 plane;
        protected int[,] worldMap;        
        bool needUpdate = true;

        public event Action<int, RaycasterUpdateResult> OnEndForUpdate;

        public List<RaycasterUpdateResult> Results { get; private set; }
        public Vector2 Camera { get; private set; }

        public Raycaster(RaycasterParameter parameter)
        {
            viewport = parameter.Viewport;
            worldMap = parameter.WorldMap;
            position = parameter.PlayerStartPosition;
            direction = parameter.CameraInitialDirection;
            plane = parameter.CameraPlane;

            mapSize.X = worldMap.GetLength(0);
            mapSize.Y = worldMap.GetLength(1);

            Results = new List<RaycasterUpdateResult>(viewport.Width);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!needUpdate)
                return;

            needUpdate = false;
            Results.Clear();

            for (int x = 0; x < viewport.Width; x++)
            {
                var camera = new Vector2(2 * x / (float)viewport.Width - 1F, 0);                
                
                var rayDirection = new Vector2(
                    direction.X + plane.X * camera.X,
                    direction.Y + plane.Y * camera.X);
                
                var map = position.ToPoint();
                var sideDistance = Vector2.Zero;

                var deltaDistance = new Vector2(
                    (rayDirection.X == 0) ? 0 : Math.Abs(1F / rayDirection.X),
                    (rayDirection.Y == 0) ? 0 : Math.Abs(1F / rayDirection.Y));
                
                var step = Point.Zero;
                var hit = 0;
                var side = 0;

                if(rayDirection.X < 0)
                {
                    step.X = -1;
                    sideDistance.X = (position.X - map.X) * deltaDistance.X;
                }
                else
                {
                    step.X = 1;
                    sideDistance.X = (map.X + 1F - position.X) * deltaDistance.X;
                }

                if(rayDirection.Y < 0)
                {
                    step.Y = -1;
                    sideDistance.Y = (position.Y - map.Y) * deltaDistance.Y;
                }
                else
                {
                    step.Y = 1;
                    sideDistance.Y = (map.Y + 1F - position.Y) * deltaDistance.Y;
                }

                while(hit == 0)
                {
                    if(sideDistance.X < sideDistance.Y)
                    {
                        sideDistance.X += deltaDistance.X;
                        map.X += step.X;
                        side = 0;
                    }
                    else
                    {
                        sideDistance.Y += deltaDistance.Y;
                        map.Y += step.Y;
                        side = 1;
                    }

                    if (worldMap[map.X, map.Y] > 0)
                        hit = 1;
                }

                float perpendicularWallDistance;

                if (side == 0)
                    perpendicularWallDistance = sideDistance.X - deltaDistance.X;
                else
                    perpendicularWallDistance = sideDistance.Y - deltaDistance.Y;

                int lineHeight = (int)(viewport.Height / perpendicularWallDistance);                

                int drawStart = -lineHeight / 2 + viewport.Height / 2;

                //if (drawStart < 0)
                //    drawStart = 0;

                int drawEnd = lineHeight / 2 + viewport.Height / 2;

                //if (drawEnd >= viewport.Height)
                //    drawEnd = viewport.Height - 1;

                Camera = position;

                var result = new RaycasterUpdateResult
                {
                    Line = new RaycasterLine(drawStart, drawEnd, lineHeight),
                    Side = side,
                    Point = map,
                    PerpendicularWallDistance = perpendicularWallDistance,
                    RayDirection = rayDirection,
                    LineHeight = lineHeight,
                };                

                OnEndForUpdate?.Invoke(x, result);
                Results.Add(result);
            }            
        }

        public void MoveForward(float moveSpeed)
        {
            int x = (int)(position.X + direction.X * moveSpeed);
            int y = (int)position.Y;            

            if (worldMap[x, y] == 0)
                position.X += direction.X * moveSpeed;

            x = (int)position.X;
            y = (int)(position.Y + direction.Y * moveSpeed);

            if (worldMap[x, y] == 0)
                position.Y += direction.Y * moveSpeed;           

            needUpdate = true;
        }

        public void MoveBack(float moveSpeed)
        {
            int x = (int)(position.X - direction.X * moveSpeed);
            int y = (int)position.Y;

            if (worldMap[x, y] == 0)
                position.X -= direction.X * moveSpeed;

            x = (int)position.X;
            y = (int)(position.Y - direction.Y * moveSpeed);

            if (worldMap[x, y] == 0)
                position.Y -= direction.Y * moveSpeed;

            if (position.X < 5)
                position.X = 5;
            if (position.Y < 5)
                position.Y = 5;

            needUpdate = true;
        }

        public void MoveRight(float rotationSpeed)
        {
            double oldDirX = direction.X;
            
            var x = direction.X * Math.Cos(-rotationSpeed) - direction.Y * Math.Sin(-rotationSpeed);
            var y = oldDirX * Math.Sin(-rotationSpeed) + direction.Y * Math.Cos(-rotationSpeed);

            direction.X = (float)x;
            direction.Y = (float)y;
            
            double oldPlaneX = plane.X;
            var pX = plane.X * Math.Cos(-rotationSpeed) - plane.Y * Math.Sin(-rotationSpeed);
            var pY = oldPlaneX * Math.Sin(-rotationSpeed) + plane.Y * Math.Cos(-rotationSpeed);

            plane.X = (float)pX;
            plane.Y = (float)pY;

            needUpdate = true;
        }

        public void MoveLeft(float rotationSpeed)
        {
            double oldDirX = direction.X;

            var x = direction.X * Math.Cos(rotationSpeed) - direction.Y * Math.Sin(rotationSpeed);
            var y = oldDirX * Math.Sin(rotationSpeed) + direction.Y * Math.Cos(rotationSpeed);

            direction.X = (float)x;
            direction.Y = (float)y;

            double oldPlaneX = plane.X;
            var pX = plane.X * Math.Cos(rotationSpeed) - plane.Y * Math.Sin(rotationSpeed);
            var pY = oldPlaneX * Math.Sin(rotationSpeed) + plane.Y * Math.Cos(rotationSpeed);

            plane.X = (float)pX;
            plane.Y = (float)pY;

            needUpdate = true;
        }
    }
}
