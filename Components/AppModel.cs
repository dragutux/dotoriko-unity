using UnityEngine;
using System.Collections;
using DotOriko.Data;
using DotOriko.Data.Serialization;

namespace DotOriko.Components {
	public sealed class AppModel : Config<AppModel> {
	    public const string FILE_NAME = "usersave";

	    public AppModel() : base(FILE_NAME) {
	        this.playerData = new PlayerData();
	        this.settings = new Settings();
	    }

	    public PlayerData playerData { get; set; }

	    public Settings settings { get; set; }
	}

	public class PlayerData {
	    public int coins { get; set; }

	    public PlayerData() {
	        this.coins = 0;
	    }
	}

	public class Settings {
	    public bool fxOn { get; set; }
	    public bool musicOn { get; set; }

	    public Settings() {
	        this.fxOn = true;
	        this.musicOn = true;
	    }
	}
}
