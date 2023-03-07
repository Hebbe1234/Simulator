using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PrimitiveType = Microsoft.Xna.Framework.Graphics.PrimitiveType;

namespace P8_Simulator.Data
{
    internal class Graph<T>
    {
        private Matrix _worldMatrix;
        private Matrix _viewMatrix;
        private Matrix _projectionMatrix;
        private BasicEffect _basicEffect;

        private VertexPositionColor[] _pointList;
        private short[] _lineListIndices;

        public Graph(ICollection<T> dataPoints, Func<T, float> yFunc, int width, int height, Color color)
        {
           CalculateGraph(dataPoints, yFunc, width, height, color);
        }

        private void CalculateGraph(ICollection<T> dataPoints, Func<T, float> yFunc, int width, int height, Color color)
        {
            int n = dataPoints.Count;
            //GeneratePoints generates a random graph, implementation irrelevant
            _pointList = new VertexPositionColor[n];
            foreach (var (item, i) in dataPoints.Select((p, i) => (p, i)))
            {
                _pointList[i] = new VertexPositionColor() { Position = new Vector3((i / (float)n) * width, (1 - yFunc(item)) * height, 0), Color = color };
            }
            //links the points into a list
            _lineListIndices = new short[(n * 2) - 2];
            for (var i = 0; i < n - 1; i++)
            {
                _lineListIndices[i * 2] = (short)(i);
                _lineListIndices[(i * 2) + 1] = (short)(i + 1);
            }
        }

        public void Draw(GraphicsDevice gd)
        {

            _worldMatrix = Matrix.Identity;
            _viewMatrix = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 1.0f), Vector3.Zero, Vector3.Up);
            _projectionMatrix = Matrix.CreateOrthographicOffCenter(0, gd.Viewport.Width, gd.Viewport.Height, 0, 1.0f, 1000.0f);

            _basicEffect = new BasicEffect(gd);
            _basicEffect.World = _worldMatrix;
            _basicEffect.View = _viewMatrix;
            _basicEffect.Projection = _projectionMatrix;

            _basicEffect.VertexColorEnabled = true; //important for color

            foreach (var pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                gd.DrawUserIndexedPrimitives(
                    PrimitiveType.LineList,
                    _pointList,
                    0,
                    _pointList.Length,
                    _lineListIndices,
                    0,
                    _pointList.Length - 1
                );
            }
        }
    }
}
