﻿using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.Tags
{
	[CreateAssetMenu(menuName = "ToolBox/Tag"), AssetSelector]
	public sealed class Tag : ScriptableObject
	{
		[ShowInInspector, ReadOnly] private HashSet<int> _entities = new HashSet<int>();

		public void Add(int entity) =>
			_entities.Add(entity);

		public void Remove(int entity) =>
			_entities.Remove(entity);

		public bool HasEntity(int entity) =>
			_entities.Contains(entity);
	}

	public static class TagExtensions
	{
		public static void AddTag(this GameObject entity, Tag tag) =>
			tag.Add(entity.GetHashCode());

		public static void RemoveTag(this GameObject entity, Tag tag) =>
			tag.Remove(entity.GetHashCode());

		public static bool HasTag(this GameObject entity, Tag tag) =>
			tag.HasEntity(entity.GetHashCode());

		public static void AddTags(this GameObject entity, Tag[] tags)
		{
			int hash = entity.GetHashCode();

			for (int i = 0; i < tags.Length; i++)
				tags[i].Add(hash);
		}

		public static void RemoveTags(this GameObject entity, Tag[] tags)
		{
			int hash = entity.GetHashCode();

			for (int i = 0; i < tags.Length; i++)
				tags[i].Remove(hash);
		}

		public static bool HasTags(this GameObject entity, Tag[] tags, bool allRequired)
		{
			int hash = entity.GetHashCode();

			if (allRequired)
			{
				for (int i = 0; i < tags.Length; i++)
				{
					if (!tags[i].HasEntity(hash))
						return false;
				}

				return true;
			}
			else
			{
				for (int i = 0; i < tags.Length; i++)
				{
					if (tags[i].HasEntity(hash))
						return true;
				}

				return false;
			}
		}
	}
}

