using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace ControleModelo.Classes
{
    public class Ferramentas
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
        public static Position.PlaneEnum ConvertePositionPlaneTekla(ControleModeloPositionPlane Opcao)
        {

            switch (Opcao)
            {
                case ControleModeloPositionPlane.Middle:
                    return Position.PlaneEnum.MIDDLE;
                case ControleModeloPositionPlane.Right:
                    return Position.PlaneEnum.RIGHT;
                default:
                    return Position.PlaneEnum.LEFT;
            }
        }
        public static Position.RotationEnum ConvertePositionRotationTekla(ControleModeloPositionRotation Opcao)
        {

            switch (Opcao)
            {
                case ControleModeloPositionRotation.Top:
                    return Position.RotationEnum.TOP;
                case ControleModeloPositionRotation.Front:
                    return Position.RotationEnum.FRONT;
                case ControleModeloPositionRotation.Back:
                    return Position.RotationEnum.BACK;
                default:
                    return Position.RotationEnum.BELOW;
            }
        }
        public static Position.DepthEnum ConvertePositionDepthTekla(ControleModeloPositionDepth Opcao)
        {

            switch (Opcao)
            {
                case ControleModeloPositionDepth.Middle:
                    return Position.DepthEnum.MIDDLE;
                case ControleModeloPositionDepth.Front:
                    return Position.DepthEnum.FRONT;
                default:
                    return Position.DepthEnum.BEHIND;
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


