﻿using System;
using System.Collections.ObjectModel;
using ToolBox.Attributes;
using UnityEngine;

namespace ToolBox.Groups
{
	[DisallowMultipleComponent]
	public class Member : MonoBehaviour
	{
		[SerializeField, ReorderableList, BoxGroup("My Groups")] private Group[] groups = null;
		[SerializeField, ReadOnly, BoxGroup("Debug")] private int groupsCount = 0;

		private GameObject cachedGameObject = null;

		private void Awake()
		{
			cachedGameObject = gameObject;
			groupsCount = groups.Length;

			if (groupsCount <= 0)
			{
				Debug.LogError("I have 0 groups", cachedGameObject);
				enabled = false;
			}
		}

		private void OnEnable()
		{
			for (int i = 0; i < groupsCount; i++)
				groups[i].AddMember(cachedGameObject);
		}

		private void OnDisable()
		{
			for (int i = 0; i < groupsCount; i++)
				groups[i].RemoveMember(cachedGameObject);
		}
#if UNITY_EDITOR
		public ReadOnlyCollection<Group> GetGroups()
		{
			ReadOnlyCollection<Group> groups = Array.AsReadOnly<Group>(this.groups);
			return groups;
		}
#endif
	}
}

