﻿using System.Linq;
using DesktopCS.Helpers.Extensions;
using DesktopCS.MVVM;
using NetIRC;

namespace DesktopCS.Models
{
    public class UserItem : ObservableObject
    {
        private User _user;

        public User User
        {
            get { return this._user; }
            set
            {
                this._user = value;
                this.OnPropertyChanged("User");
            }
        }

        private UserRank _rank;

        public UserRank Rank
        {
            get { return this._rank; }
            set
            {
                this._rank = value;
                this.OnPropertyChanged("Rank");
            }
        }

        public UserRank HighestRank
        {
            get
            {
                return this._rank.GetFlags().Max(); ;
            }
        }  
        
        private string _nickName;

        public string NickName
        {
            get { return this._nickName; }
            set
            {
                this._nickName = value;
                this.OnPropertyChanged("NickName");
            }
        }

        private UserMetadata _metadata;

        public UserMetadata Metadata
        {
            get { return this._metadata; }
            set
            {
                this._metadata = value;
                this.OnPropertyChanged("Metadata");
            }
        }

        private bool _isAway;

        public bool IsAway
        {
            get { return this._isAway; }
            set
            {
                this._isAway = value;
                this.OnPropertyChanged("IsAway");
            }
        }

        private string _awayMessage;

        public string AwayMessage
        {
            get { return this._awayMessage; }
            set 
            {
                this._awayMessage = value;
                this.OnPropertyChanged("AwayMessage");
            }
        }

        public UserItem(User user, UserRank rank, string nick, UserMetadata metadata, bool isAway, string awayMessage)
        {
            this.User = user;
            this.Rank = rank;
            this.NickName = nick;
            this.Metadata = metadata;
            this.IsAway = isAway;
            this.AwayMessage = awayMessage;
        }


        public static bool operator ==(UserItem x, UserItem y)
        {
            if (ReferenceEquals(null, x) || ReferenceEquals(null, y))
                return ReferenceEquals(x, y);;

            return  x.NickName == y.NickName;
        }
        public static bool operator !=(UserItem x, UserItem y)
        {
            return !(x == y);
        }

        #region Equality members

        public bool Equals(UserItem other)
        {
            return string.Equals(this.NickName, other.NickName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is UserItem && this.Equals((UserItem)obj);
        }

        public override int GetHashCode()
        {
            return (this.NickName != null ? this.NickName.GetHashCode() : 0);
        }

        #endregion
    }
}
