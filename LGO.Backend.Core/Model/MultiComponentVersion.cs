using System;
using System.Collections.Generic;
using System.Linq;

namespace LGO.Backend.Core.Model
{
    public sealed class MultiComponentVersion : IEquatable<MultiComponentVersion>, IComparable<MultiComponentVersion>, IComparable
    {
        private const string ComponentDelimiter = ".";
        
        private int[] Components { get; }

        public static bool TryParse(string value, out MultiComponentVersion multiComponentVersion)
        {
            multiComponentVersion = null!;
            try
            {
                multiComponentVersion = Parse(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static MultiComponentVersion Parse(string value)
        {
            var tokens = value.Split(ComponentDelimiter);
            var components = new List<int>();
            
            foreach (var token in tokens)
            {
                if (!int.TryParse(token, out var component))
                {
                    throw new ArgumentException($"Unable to parse {value}.");
                }
                
                components.Add(component);
            }

            if (components.Count < 1)
            {
                throw new ArgumentException($"{nameof(value)} must not be empty.");
            }

            return new MultiComponentVersion(components.ToArray());
        }

        public MultiComponentVersion(params int[] components)
        {
            Components = components;
            
            if (Components.Length < 1)
            {
                throw new ArgumentException($"{nameof(components)} must not be empty.");
            }

            if (Components.Any(component => component < 0))
            {
                throw new ArgumentOutOfRangeException($"All of the given {nameof(components)} must be greater or equal to 0.");
            }
        }

        public override string ToString()
        {
            return string.Join(ComponentDelimiter, Components);
        }

        public int CompareTo(object? obj)
        {
            return CompareTo(obj as MultiComponentVersion ?? throw new ArgumentException($"{nameof(obj)} must be of type {nameof(MultiComponentVersion)}."));
        }

        public int CompareTo(MultiComponentVersion? other)
        {
            if (other == null)
            {
                return 1;
            }

            for (var i = 0; i < Math.Max(Components.Length, other.Components.Length); ++i)
            {
                var componentA = i < Components.Length ? Components[i] : 0;
                var componentB = i < other.Components.Length ? other.Components[i] : 0;

                if (componentA < componentB)
                {
                    return -1;
                }

                if (componentA > componentB)
                {
                    return 1;
                }
            }

            return 0;
        }

        public static bool operator <(MultiComponentVersion? a, MultiComponentVersion? b)
        {
            return a?.CompareTo(b) < 0;
        }

        public static bool operator >(MultiComponentVersion? a, MultiComponentVersion? b)
        {
            return a?.CompareTo(b) > 0;
        }

        public static bool operator <=(MultiComponentVersion? a, MultiComponentVersion? b)
        {
            return a?.CompareTo(b) <= 0;
        }

        public static bool operator >=(MultiComponentVersion? a, MultiComponentVersion? b)
        {
            return a?.CompareTo(b) >= 0;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as MultiComponentVersion);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public bool Equals(MultiComponentVersion? other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            for (var i = 0; i < Math.Max(Components.Length, other.Components.Length); ++i)
            {
                var componentA = i < Components.Length ? Components[i] : 0;
                var componentB = i < other.Components.Length ? other.Components[i] : 0;

                if (componentA != componentB)
                {
                    return false;
                }
            }

            return true;
        }
        

        public static bool operator ==(MultiComponentVersion? a, MultiComponentVersion? b)
        {
            return a?.Equals(b) == true;
        }

        public static bool operator !=(MultiComponentVersion? a, MultiComponentVersion? b)
        {
            return a?.Equals(b) == false;
        }
    }
}