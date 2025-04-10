using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace ControleModelo.Classes
{
    internal class Ferramentas
    {
        public static ControleModeloPositionPlane ConvertePositionPlane(Position.PlaneEnum Opcao)
        {

            switch (Opcao)
            {
                case Position.PlaneEnum.MIDDLE:
                    return ControleModeloPositionPlane.Middle;
                case Position.PlaneEnum.RIGHT:
                    return ControleModeloPositionPlane.Right;
                default:
                    return ControleModeloPositionPlane.Left;
            }
        }
        public static ControleModeloPositionRotation ConvertePositionRotation(Position.RotationEnum Opcao)
        {

            switch (Opcao)
            {
                case Position.RotationEnum.FRONT:
                    return ControleModeloPositionRotation.Front;
                case Position.RotationEnum.BACK:
                    return ControleModeloPositionRotation.Back;
                case Position.RotationEnum.BELOW:
                    return ControleModeloPositionRotation.Below;
                default:
                    return ControleModeloPositionRotation.Top;
            }
        }
        public static ControleModeloPositionDepth ConvertePositionDepth(Position.DepthEnum Opcao)
        {

            switch (Opcao)
            {
                case Position.DepthEnum.MIDDLE:
                    return ControleModeloPositionDepth.Middle;
                case Position.DepthEnum.FRONT:
                    return ControleModeloPositionDepth.Front;
                default:
                    return ControleModeloPositionDepth.Behind;
            }
        }
    }
}


