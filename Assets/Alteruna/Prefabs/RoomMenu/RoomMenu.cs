using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Alteruna.Trinity;

namespace Alteruna
{
	public class RoomMenu : CommunicationBridge
	{
		[SerializeField] private Text TitleText;
		[SerializeField] private GameObject LANEntryPrefab;
		[SerializeField] private GameObject WANEntryPrefab;
		[SerializeField] private GameObject ContentContainer;
		[SerializeField] private Button StartButton;
		[SerializeField] private Button LeaveButton;

		// manual refresh can be done by calling Multiplayer.RefreshRoomList();
		public bool AutomaticallyRefresh = true;
		public float RefreshInterval = 5.0f;
		
		private List<RoomObject> _roomObjects = new List<RoomObject>();
		private float _refreshTime;

		private int count;
		private string _connectionMessage = "Connecting";
		private float _statusTextTime;
		private int _roomI = -1;

		public bool JoinRoom(string roomName, ushort password = 0)
		{
			roomName = roomName.ToLower();
			if (Multiplayer != null && Multiplayer.IsConnected)
			{
				foreach (var room in Multiplayer.AvailableRooms)
				{
					if (room.Name.ToLower() == roomName)
					{
						room.Join(password);
						return true;
					}
				}
			}

			return false;
		}

		private void Connected(Multiplayer multiplayer, Endpoint endpoint)
		{

			// if already connected to room
			if (multiplayer.InRoom)
			{
				JoinedRoom(multiplayer, multiplayer.CurrentRoom, multiplayer.Me);
				return;
			}

			StartButton.interactable = true;
			LeaveButton.interactable = false;

			if (TitleText != null)
			{
				TitleText.text = "Rooms";
			}
		}

		private void Disconnected(Multiplayer multiplayer, Endpoint endPoint)
		{
			StartButton.interactable = false;
			LeaveButton.interactable = false;

			_connectionMessage = "Reconnecting";
			if (TitleText != null)
			{
				TitleText.text = "Reconnecting";
			}
		}

		private void UpdateList(Multiplayer multiplayer)
		{
			if (ContentContainer == null) return;

			bool roomChange = false;
			{
				int roomI = Multiplayer.InRoom ? (int)Multiplayer.CurrentRoom.ID : -1;
				if (roomI != _roomI)
				{
					_roomI = roomI;
					roomChange = true;
				}
			}

			if (multiplayer.AvailableRooms.Count < _roomObjects.Count)
			{
				for (int i = _roomObjects.Count - 1; i >= multiplayer.AvailableRooms.Count; i--)
				{
					Destroy(_roomObjects[i].GameObject);
					_roomObjects.RemoveAt(i);
				}
			}

			for (int i = 0; i < multiplayer.AvailableRooms.Count; i++)
			{
				Room room = multiplayer.AvailableRooms[i];
				RoomObject entry;
				Button button;

				if (_roomObjects.Count > i)
				{
					if (room.Local != _roomObjects[i].Lan)
					{
						Destroy(_roomObjects[i].GameObject);
						if (room.Local)
						{
							entry = new RoomObject(Instantiate(LANEntryPrefab, ContentContainer.transform), true);
						}
						else
						{
							entry = new RoomObject(Instantiate(WANEntryPrefab, ContentContainer.transform));
						}

						_roomObjects[i] = entry;
						button = entry.GameObject.GetComponentInChildren<Button>();
					}
					else
					{
						// If unchanged, then skip to next room.
						if (roomChange || (room.ID == multiplayer.AvailableRooms[i].ID && room.Name == multiplayer.AvailableRooms[i].Name)) continue;
						entry = _roomObjects[i];
						button = entry.GameObject.GetComponentInChildren<Button>();
						button.onClick.RemoveAllListeners();
					}
				}
				else
				{
					if (room.Local)
					{
						entry = new RoomObject(Instantiate(LANEntryPrefab, ContentContainer.transform), true);
					}
					else
					{
						entry = new RoomObject(Instantiate(WANEntryPrefab, ContentContainer.transform));
					}

					button = entry.GameObject.GetComponentInChildren<Button>();

					_roomObjects.Add(entry);
				}

				// Hide private rooms.
				if (room.InviteOnly && room.ID != _roomI)
				{
					entry.GameObject.SetActive(false);
					continue;
				}

				entry.GameObject.SetActive(true);
				entry.GameObject.name = room.Name;
				entry.GameObject.GetComponentInChildren<Text>().text = room.Name;

				if (room.ID == _roomI)
				{
					button.interactable = false;
				}
				else
				{
					button.interactable = true;
					button.onClick.AddListener(() =>
					{
						room.Join();
						UpdateList(multiplayer);
					});
				}
			}
		}

		private void JoinedRoom(Multiplayer multiplayer, Room room, User user)
		{
			StartButton.interactable = false;
			LeaveButton.interactable = true;

			if (TitleText != null)
			{
				TitleText.text = "In Room " + room.Name;
			}
		}

		private void LeftRoom(Multiplayer multiplayer)
		{
			StartButton.interactable = true;
			LeaveButton.interactable = false;

			if (TitleText != null)
			{
				TitleText.text = "Rooms";
			}
		}

		private void FixedUpdate()
		{
			if (!Multiplayer.enabled)
			{
				TitleText.text = "Offline";
			}
			else if (Multiplayer.IsConnected)
			{
				if (!AutomaticallyRefresh || (_refreshTime += Time.fixedDeltaTime) < RefreshInterval) return;
				_refreshTime -= RefreshInterval;

				Multiplayer.RefreshRoomList();

				if (TitleText == null) return;

				ResponseCode blockedReason = Multiplayer.GetLastBlockResponse();

				if (blockedReason == ResponseCode.NaN) return;

				string str = blockedReason.ToString();
				str = string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
				TitleText.text = str;
			}
			else if ((_statusTextTime += Time.fixedDeltaTime) >= 1)
			{
				_statusTextTime -= 1;
				ResponseCode blockedReason = Multiplayer.GetLastBlockResponse();
				if (blockedReason != ResponseCode.NaN)
				{
					string str = blockedReason.ToString();
					str = string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
					TitleText.text = str;
					return;
				}

				switch (count)
				{
					case 0:
						TitleText.text = _connectionMessage + ".  ";
						break;
					case 1:
						TitleText.text = _connectionMessage + ".. ";
						break;
					default:
						TitleText.text = _connectionMessage + "...";
						count = -1;
						break;
				}

				count++;
			}
		}

		private void Start()
		{
			if (Multiplayer == null)
			{
				Multiplayer = FindObjectOfType<Multiplayer>();
			}

			if (Multiplayer == null)
			{

				Debug.LogError("Unable to find a active object of type Multiplayer.");
				if (TitleText != null) TitleText.text = "Missing Multiplayer Component";
				enabled = false;
			}
			else
			{
				Multiplayer.Connected.AddListener(Connected);
				Multiplayer.Disconnected.AddListener(Disconnected);
				Multiplayer.RoomListUpdated.AddListener(UpdateList);
				Multiplayer.RoomJoined.AddListener(JoinedRoom);
				Multiplayer.RoomLeft.AddListener(LeftRoom);

				StartButton.onClick.AddListener(() =>
				{
					Multiplayer.JoinOnDemandRoom();
					_refreshTime = RefreshInterval;
				});

				LeaveButton.onClick.AddListener(() =>
				{
					Multiplayer.CurrentRoom?.Leave();
					_refreshTime = RefreshInterval;
				});

				if (TitleText != null)
				{
					ResponseCode blockedReason = Multiplayer.GetLastBlockResponse();
					if (blockedReason != ResponseCode.NaN)
					{
						string str = blockedReason.ToString();
						str = string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
						TitleText.text = str;
					}
					else
					{
						TitleText.text = "Connecting";
					}
				}

				// if already connected
				if (Multiplayer.IsConnected)
				{
					Connected(Multiplayer, null);
				}
			}

			StartButton.interactable = false;
			LeaveButton.interactable = false;
		}

		private struct RoomObject
		{
			public readonly GameObject GameObject;
			public readonly bool Lan;

			public RoomObject(GameObject obj, bool lan = false)
			{
				GameObject = obj;
				Lan = lan;
			}
		}
	}
}