using ConferenceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public static class EntityExtensions
    {
        public static SessionResponse MapSessionResponse(this Session session) =>
            new SessionResponse
            {
                Id = session.Id,
                Title = session.Title,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Speakers = session.SessionSpeakers?
                    .Select(ss => new ConferenceDTO.Speaker
                    {
                        Id = ss.SpeakerId,
                        Name = ss.Speaker.Name
                    }).ToList(),
                TrackId = session.TrackId,
                Track = new ConferenceDTO.Track
                {
                    Id = session?.TrackId ?? 0,
                    Name = session.Track?.Name
                },
                Abstract = session.Abstract
            };

        public static SpeakerResponse MapSpeakerResponse(this Speaker speaker) =>
            new SpeakerResponse
            {
                Id = speaker.Id,
                Name = speaker.Name,
                Bio = speaker.Bio,
                WebSite = speaker.WebSite,
                Sessions = speaker.SessionSpeakers?
                    .Select(ss =>
                        new ConferenceDTO.Session
                        {
                            Id = ss.SessionId,
                            Title = ss.Session.Title
                        }).ToList()
            };

        public static AttendeeResponse MapAttendeeResponse(this Attendee attendee) =>
            new AttendeeResponse
            {
                Id = attendee.Id,
                FirstName = attendee.FirstName,
                LastName = attendee.LastName,
                UserName = attendee.UserName,
                Sessions = attendee.SessionsAttendees?
                    .Select(sa =>
                        new ConferenceDTO.Session
                        {
                            Id = sa.SessionId,
                            Title = sa.Session.Title,
                            StartTime = sa.Session.StartTime,
                            EndTime = sa.Session.EndTime
                        }).ToList()
            };
    }
}