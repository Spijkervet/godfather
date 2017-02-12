using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGodfatherGM.Data
{
    /*
    [ComplexType]
    public class SerializableVector3
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        [NotMapped]
        public bool IsZero => X == 0 && Y == 0 && Z == 0;

        public static implicit operator SerializableVector3(Vector3 vector3)
        {
            return new SerializableVector3 { X = vector3.X, Y = vector3.Y, Z = vector3.Z };
        }

        public static implicit operator Vector3(SerializableVector3 serializableVector3)
        {
            return new Vector3 { X = serializableVector3.X, Y = serializableVector3.Y, Z = serializableVector3.Z };
        }
    }*/

    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        public int Model { get; set; }
        //public SerializableVector3 Position { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float Rot { get; set; }
        public int Color1 { get; set; }
        public int Color2 { get; set; }
        public bool Respawnable { get; set; }

        public int? CharacterId { get; set; }
        public virtual Character Character { get; set; }
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int? JobId { get; set; }
        public virtual Job Job { get; set; }

        public Vehicle()
        {
        }
    }
}
