using System;
using System.Collections.Generic;
using System.Text;

namespace Domain;


public enum BookingStatus
{
    Pending = 1,
    Confirmed = 2,
    CheckedIn = 3,
    CheckedOut = 4,
    Completed = 5,
    Cancelled = 6,
    NoShow = 7
}

public enum PaymentStatus
{
    Pending = 1,
    Paid = 2,
    PartiallyPaid = 3,
    Refunded = 4,
    Failed = 5
}

public enum PaymentMethod
{
    CreditCard = 1,
    DebitCard = 2,
    PayPal = 3,
    BankTransfer = 4,
    Cash = 5,
    Other = 6
}

public enum NotificationType
{
    General = 1,
    BookingConfirmation = 2,
    BookingReminder = 3,
    PaymentReceived = 4,
    ReviewRequest = 5,
    SpecialOffer = 6,
    SystemUpdate = 7,
    AdminAnnouncement = 8
}

public enum NotificationPriority
{
    Low = 1,
    Normal = 2,
    High = 3,
    Urgent = 4
}

public enum NotificationTarget
{
    AllUsers = 1,
    SpecificUser = 2,
    City = 3,
    Unit = 4,
    Role = 5
}