using  System;
using  System.Collections.Generic;
using  System.Linq.Expressions;
using  System.Web;
using  Umbraco.Core.Models;
using  Umbraco.Core.Models.PublishedContent;
using  Umbraco.Web;
using  Umbraco.ModelsBuilder;
using  Umbraco.ModelsBuilder.Umbraco;
[assembly: PureLiveAssembly, System.Reflection.AssemblyVersion("0.0.0.2")]


// FILE: models.generated.cs

//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v3.0.2.93
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------













namespace Umbraco.Web.PublishedContentModels
{
	/// <summary>Test Page</summary>
	[PublishedContentModel("testPage")]
	public partial class TestPage : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "testPage";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public TestPage(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<TestPage, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// test
		///</summary>
		[ImplementPropertyType("test")]
		public object Test
		{
			get { return this.GetPropertyValue("test"); }
		}
	}

	/// <summary>Bookable item</summary>
	[PublishedContentModel("bookableItem")]
	public partial class BookableItem : PublishedContentModel, IBookingSettings
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "bookableItem";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public BookableItem(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<BookableItem, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Booking Calender
		///</summary>
		[ImplementPropertyType("bookingCalender")]
		public object BookingCalender
		{
			get { return this.GetPropertyValue("bookingCalender"); }
		}

		///<summary>
		/// Enable booking: Enable booking for this item
		///</summary>
		[ImplementPropertyType("enableBooking")]
		public bool EnableBooking
		{
			get { return this.GetPropertyValue<bool>("enableBooking"); }
		}

		///<summary>
		/// Availability per slot: This is the total number of bookings allowed per slot. Leave blank if there is no limit.
		///</summary>
		[ImplementPropertyType("availabilityPerSlot")]
		public int AvailabilityPerSlot
		{
			get { return BookingSettings.GetAvailabilityPerSlot(this); }
		}

		///<summary>
		/// Daily end time
		///</summary>
		[ImplementPropertyType("dailyEndTime")]
		public object DailyEndTime
		{
			get { return BookingSettings.GetDailyEndTime(this); }
		}

		///<summary>
		/// Daily start time
		///</summary>
		[ImplementPropertyType("dailyStartTime")]
		public object DailyStartTime
		{
			get { return BookingSettings.GetDailyStartTime(this); }
		}

		///<summary>
		/// Enabled booking: Select this option to enable booking for this item
		///</summary>
		[ImplementPropertyType("enabledBooking")]
		public bool EnabledBooking
		{
			get { return BookingSettings.GetEnabledBooking(this); }
		}

		///<summary>
		/// Minimum booking length
		///</summary>
		[ImplementPropertyType("minimumBookingLength")]
		public int MinimumBookingLength
		{
			get { return BookingSettings.GetMinimumBookingLength(this); }
		}

		///<summary>
		/// Minimum booking time period
		///</summary>
		[ImplementPropertyType("minimumBookingTimePeriod")]
		public object MinimumBookingTimePeriod
		{
			get { return BookingSettings.GetMinimumBookingTimePeriod(this); }
		}
	}

	// Mixin content Type 1054 with alias "bookingSettings"
	/// <summary>Booking Settings [Timeslot]</summary>
	public partial interface IBookingSettings : IPublishedContent
	{
		/// <summary>Availability per slot</summary>
		int AvailabilityPerSlot { get; }

		/// <summary>Daily end time</summary>
		object DailyEndTime { get; }

		/// <summary>Daily start time</summary>
		object DailyStartTime { get; }

		/// <summary>Enabled booking</summary>
		bool EnabledBooking { get; }

		/// <summary>Minimum booking length</summary>
		int MinimumBookingLength { get; }

		/// <summary>Minimum booking time period</summary>
		object MinimumBookingTimePeriod { get; }
	}

	/// <summary>Booking Settings [Timeslot]</summary>
	[PublishedContentModel("bookingSettings")]
	public partial class BookingSettings : PublishedContentModel, IBookingSettings
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "bookingSettings";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public BookingSettings(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<BookingSettings, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Availability per slot: This is the total number of bookings allowed per slot. Leave blank if there is no limit.
		///</summary>
		[ImplementPropertyType("availabilityPerSlot")]
		public int AvailabilityPerSlot
		{
			get { return GetAvailabilityPerSlot(this); }
		}

		/// <summary>Static getter for Availability per slot</summary>
		public static int GetAvailabilityPerSlot(IBookingSettings that) { return that.GetPropertyValue<int>("availabilityPerSlot"); }

		///<summary>
		/// Daily end time
		///</summary>
		[ImplementPropertyType("dailyEndTime")]
		public object DailyEndTime
		{
			get { return GetDailyEndTime(this); }
		}

		/// <summary>Static getter for Daily end time</summary>
		public static object GetDailyEndTime(IBookingSettings that) { return that.GetPropertyValue("dailyEndTime"); }

		///<summary>
		/// Daily start time
		///</summary>
		[ImplementPropertyType("dailyStartTime")]
		public object DailyStartTime
		{
			get { return GetDailyStartTime(this); }
		}

		/// <summary>Static getter for Daily start time</summary>
		public static object GetDailyStartTime(IBookingSettings that) { return that.GetPropertyValue("dailyStartTime"); }

		///<summary>
		/// Enabled booking: Select this option to enable booking for this item
		///</summary>
		[ImplementPropertyType("enabledBooking")]
		public bool EnabledBooking
		{
			get { return GetEnabledBooking(this); }
		}

		/// <summary>Static getter for Enabled booking</summary>
		public static bool GetEnabledBooking(IBookingSettings that) { return that.GetPropertyValue<bool>("enabledBooking"); }

		///<summary>
		/// Minimum booking length
		///</summary>
		[ImplementPropertyType("minimumBookingLength")]
		public int MinimumBookingLength
		{
			get { return GetMinimumBookingLength(this); }
		}

		/// <summary>Static getter for Minimum booking length</summary>
		public static int GetMinimumBookingLength(IBookingSettings that) { return that.GetPropertyValue<int>("minimumBookingLength"); }

		///<summary>
		/// Minimum booking time period
		///</summary>
		[ImplementPropertyType("minimumBookingTimePeriod")]
		public object MinimumBookingTimePeriod
		{
			get { return GetMinimumBookingTimePeriod(this); }
		}

		/// <summary>Static getter for Minimum booking time period</summary>
		public static object GetMinimumBookingTimePeriod(IBookingSettings that) { return that.GetPropertyValue("minimumBookingTimePeriod"); }
	}

	/// <summary>Folder</summary>
	[PublishedContentModel("Folder")]
	public partial class Folder : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "Folder";
		public new const PublishedItemType ModelItemType = PublishedItemType.Media;
#pragma warning restore 0109

		public Folder(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Folder, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Contents:
		///</summary>
		[ImplementPropertyType("contents")]
		public object Contents
		{
			get { return this.GetPropertyValue("contents"); }
		}
	}

	/// <summary>Image</summary>
	[PublishedContentModel("Image")]
	public partial class Image : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "Image";
		public new const PublishedItemType ModelItemType = PublishedItemType.Media;
#pragma warning restore 0109

		public Image(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Image, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Size
		///</summary>
		[ImplementPropertyType("umbracoBytes")]
		public string UmbracoBytes
		{
			get { return this.GetPropertyValue<string>("umbracoBytes"); }
		}

		///<summary>
		/// Type
		///</summary>
		[ImplementPropertyType("umbracoExtension")]
		public string UmbracoExtension
		{
			get { return this.GetPropertyValue<string>("umbracoExtension"); }
		}

		///<summary>
		/// Upload image
		///</summary>
		[ImplementPropertyType("umbracoFile")]
		public Umbraco.Web.Models.ImageCropDataSet UmbracoFile
		{
			get { return this.GetPropertyValue<Umbraco.Web.Models.ImageCropDataSet>("umbracoFile"); }
		}

		///<summary>
		/// Height
		///</summary>
		[ImplementPropertyType("umbracoHeight")]
		public string UmbracoHeight
		{
			get { return this.GetPropertyValue<string>("umbracoHeight"); }
		}

		///<summary>
		/// Width
		///</summary>
		[ImplementPropertyType("umbracoWidth")]
		public string UmbracoWidth
		{
			get { return this.GetPropertyValue<string>("umbracoWidth"); }
		}
	}

	/// <summary>File</summary>
	[PublishedContentModel("File")]
	public partial class File : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "File";
		public new const PublishedItemType ModelItemType = PublishedItemType.Media;
#pragma warning restore 0109

		public File(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<File, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Size
		///</summary>
		[ImplementPropertyType("umbracoBytes")]
		public string UmbracoBytes
		{
			get { return this.GetPropertyValue<string>("umbracoBytes"); }
		}

		///<summary>
		/// Type
		///</summary>
		[ImplementPropertyType("umbracoExtension")]
		public string UmbracoExtension
		{
			get { return this.GetPropertyValue<string>("umbracoExtension"); }
		}

		///<summary>
		/// Upload file
		///</summary>
		[ImplementPropertyType("umbracoFile")]
		public object UmbracoFile
		{
			get { return this.GetPropertyValue("umbracoFile"); }
		}
	}

	/// <summary>Member</summary>
	[PublishedContentModel("Member")]
	public partial class Member : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "Member";
		public new const PublishedItemType ModelItemType = PublishedItemType.Member;
#pragma warning restore 0109

		public Member(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Member, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Is Approved
		///</summary>
		[ImplementPropertyType("umbracoMemberApproved")]
		public bool UmbracoMemberApproved
		{
			get { return this.GetPropertyValue<bool>("umbracoMemberApproved"); }
		}

		///<summary>
		/// Comments
		///</summary>
		[ImplementPropertyType("umbracoMemberComments")]
		public string UmbracoMemberComments
		{
			get { return this.GetPropertyValue<string>("umbracoMemberComments"); }
		}

		///<summary>
		/// Failed Password Attempts
		///</summary>
		[ImplementPropertyType("umbracoMemberFailedPasswordAttempts")]
		public string UmbracoMemberFailedPasswordAttempts
		{
			get { return this.GetPropertyValue<string>("umbracoMemberFailedPasswordAttempts"); }
		}

		///<summary>
		/// Last Lockout Date
		///</summary>
		[ImplementPropertyType("umbracoMemberLastLockoutDate")]
		public string UmbracoMemberLastLockoutDate
		{
			get { return this.GetPropertyValue<string>("umbracoMemberLastLockoutDate"); }
		}

		///<summary>
		/// Last Login Date
		///</summary>
		[ImplementPropertyType("umbracoMemberLastLogin")]
		public string UmbracoMemberLastLogin
		{
			get { return this.GetPropertyValue<string>("umbracoMemberLastLogin"); }
		}

		///<summary>
		/// Last Password Change Date
		///</summary>
		[ImplementPropertyType("umbracoMemberLastPasswordChangeDate")]
		public string UmbracoMemberLastPasswordChangeDate
		{
			get { return this.GetPropertyValue<string>("umbracoMemberLastPasswordChangeDate"); }
		}

		///<summary>
		/// Is Locked Out
		///</summary>
		[ImplementPropertyType("umbracoMemberLockedOut")]
		public bool UmbracoMemberLockedOut
		{
			get { return this.GetPropertyValue<bool>("umbracoMemberLockedOut"); }
		}

		///<summary>
		/// Password Answer
		///</summary>
		[ImplementPropertyType("umbracoMemberPasswordRetrievalAnswer")]
		public string UmbracoMemberPasswordRetrievalAnswer
		{
			get { return this.GetPropertyValue<string>("umbracoMemberPasswordRetrievalAnswer"); }
		}

		///<summary>
		/// Password Question
		///</summary>
		[ImplementPropertyType("umbracoMemberPasswordRetrievalQuestion")]
		public string UmbracoMemberPasswordRetrievalQuestion
		{
			get { return this.GetPropertyValue<string>("umbracoMemberPasswordRetrievalQuestion"); }
		}
	}

}



// EOF
