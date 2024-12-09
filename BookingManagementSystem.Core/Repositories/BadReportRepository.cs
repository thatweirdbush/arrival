using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Repositories;
public class BadReportRepository : Repository<BadReport>
{
    public BadReportRepository()
    {
        _entities.AddRange(
        [
            new() {
                Id = 1,
                UserId = 1,
                EntityId = 5001,
                ReportReason = "Inappropriate Content",
                Description = "This review was flagged by several users for including explicit and offensive language, which violates our community guidelines. The user made multiple derogatory remarks about the property host and used highly inappropriate words to describe their stay. \r\nThis review not only reflects poor judgment but also breaches our platform's guidelines on respectful and constructive feedback. Instead of focusing on the specific issues they encountered, the user went on to insult the host’s appearance and personal qualities, which is entirely irrelevant to the property's service and quality. After investigating the report, we found that the feedback did not contribute positively and might have been exaggerated or posted in retaliation. It’s critical that reviews stay professional and fact-based, especially on a platform where other users rely on honest and fair assessments to make booking decisions. \r\nFor these reasons, the review was temporarily hidden while we address the user’s behavior through appropriate channels.",
                Status = ReportStatus.Pending,
                EntityType = EntityType.Review
            },
            new() {
                Id = 2,
                UserId = 2,
                EntityId = 5002,
                ReportReason = "Spam",
                Description = "The property description claimed to offer amenities like a swimming pool, gym, and 24-hour room service, but guests who arrived found these services unavailable. Instead, the property was in an unfinished building, with construction noises disturbing the surroundings. No on-site gym or pool facilities existed, and no room service was provided. Multiple guests have filed similar complaints, which makes it apparent that the listing was deliberately misleading. \r\nSuch claims could harm our platform's reputation, as users rely on accurate listings to make informed booking decisions. Photos in the listing also did not match the actual rooms, showing much larger spaces than what is realistically available. \r\nWe contacted the host to clarify the situation, but they failed to provide a reasonable explanation for the discrepancy. Given the number of complaints and evidence from recent guests, this report highlights the need to ensure that the listing complies with platform standards and accurately represents what future guests can expect. \r\nWe are temporarily suspending the listing and issuing a warning to the host to revise all false information immediately.",
                Status = ReportStatus.Approved,
                EntityType = EntityType.Review,
                HandledByAdminId = 2001,
                HandledDate = DateTime.Now.ToUniversalTime().AddDays(-2),
                AdminNotes = "The user repeatedly messaged another user on our platform, making them feel uncomfortable with unsolicited personal questions. Despite being politely asked to stop, the user continued to contact the reporting party through our messaging system. \r\nIn one instance, the user made unwelcome comments about the reporting user’s photos on their profile, which escalated into making remarks that were perceived as threatening. This behavior directly violates our platform’s code of conduct, which promotes a safe and respectful environment. \r\nAs the messages were not related to the property booking or related queries, they are considered harassment under our community guidelines. The reporting user blocked the offender and raised this report with screenshots as evidence. \r\nGiven the severity of the messages, it was escalated for administrative review. Any form of harassment is taken seriously, and the reported messages are now being reviewed with the intent to enforce consequences that align with our anti-harassment policy."
            },
            new() {
                Id = 3,
                UserId = 3,
                EntityId = 3001,
                ReportReason = "Harassment",
                Description = "The property description claimed to offer amenities like a swimming pool, gym, and 24-hour room service, but guests who arrived found these services unavailable. Instead, the property was in an unfinished building, with construction noises disturbing the surroundings. No on-site gym or pool facilities existed, and no room service was provided. Multiple guests have filed similar complaints, which makes it apparent that the listing was deliberately misleading. \r\nSuch claims could harm our platform's reputation, as users rely on accurate listings to make informed booking decisions. Photos in the listing also did not match the actual rooms, showing much larger spaces than what is realistically available. \r\nWe contacted the host to clarify the situation, but they failed to provide a reasonable explanation for the discrepancy. Given the number of complaints and evidence from recent guests, this report highlights the need to ensure that the listing complies with platform standards and accurately represents what future guests can expect. \r\nWe are temporarily suspending the listing and issuing a warning to the host to revise all false information immediately.",
                Status = ReportStatus.Rejected,
                EntityType = EntityType.User,
                HandledByAdminId = 2002,
                HandledDate = DateTime.Now.ToUniversalTime().AddDays(-5),
                AdminNotes = "Insufficient evidence for harassment. Report rejected."
            },
            new() {
                Id = 4,
                UserId = 4,
                EntityId = 4003,
                ReportReason = "Fraud",
                Description = "This review was flagged by several users for including explicit and offensive language, which violates our community guidelines. The user made multiple derogatory remarks about the property host and used highly inappropriate words to describe their stay. \r\nThis review not only reflects poor judgment but also breaches our platform's guidelines on respectful and constructive feedback. Instead of focusing on the specific issues they encountered, the user went on to insult the host’s appearance and personal qualities, which is entirely irrelevant to the property's service and quality. After investigating the report, we found that the feedback did not contribute positively and might have been exaggerated or posted in retaliation. It’s critical that reviews stay professional and fact-based, especially on a platform where other users rely on honest and fair assessments to make booking decisions. \r\nFor these reasons, the review was temporarily hidden while we address the user’s behavior through appropriate channels.",
                Status = ReportStatus.Pending,
                EntityType = EntityType.Property
            },
            new() {
                Id = 5,
                UserId = 5,
                EntityId = 5003,
                ReportReason = "Spam",
                Description = "The user repeatedly messaged another user on our platform, making them feel uncomfortable with unsolicited personal questions. Despite being politely asked to stop, the user continued to contact the reporting party through our messaging system. \r\nIn one instance, the user made unwelcome comments about the reporting user’s photos on their profile, which escalated into making remarks that were perceived as threatening. This behavior directly violates our platform’s code of conduct, which promotes a safe and respectful environment. \r\nAs the messages were not related to the property booking or related queries, they are considered harassment under our community guidelines. The reporting user blocked the offender and raised this report with screenshots as evidence. \r\nGiven the severity of the messages, it was escalated for administrative review. Any form of harassment is taken seriously, and the reported messages are now being reviewed with the intent to enforce consequences that align with our anti-harassment policy.",
                Status = ReportStatus.Approved,
                EntityType = EntityType.User,
                HandledByAdminId = 2003,
                HandledDate = DateTime.Now.ToUniversalTime().AddDays(-1),
                AdminNotes = "User account suspended for 3 days."
            },
            new() {
                Id = 6,
                UserId = 6,
                EntityId = 4001,
                ReportReason = "Inappropriate Content",
                Description = "This review was flagged by several users for including explicit and offensive language, which violates our community guidelines. The user made multiple derogatory remarks about the property host and used highly inappropriate words to describe their stay. \r\nThis review not only reflects poor judgment but also breaches our platform's guidelines on respectful and constructive feedback. Instead of focusing on the specific issues they encountered, the user went on to insult the host’s appearance and personal qualities, which is entirely irrelevant to the property's service and quality. After investigating the report, we found that the feedback did not contribute positively and might have been exaggerated or posted in retaliation. It’s critical that reviews stay professional and fact-based, especially on a platform where other users rely on honest and fair assessments to make booking decisions. \r\nFor these reasons, the review was temporarily hidden while we address the user’s behavior through appropriate channels.",
                Status = ReportStatus.Pending,
                EntityType = EntityType.Property
            },
            new() {
                Id = 7,
                UserId = 7,
                EntityId = 3002,
                ReportReason = "Harassment",
                Description = "The property description claimed to offer amenities like a swimming pool, gym, and 24-hour room service, but guests who arrived found these services unavailable. Instead, the property was in an unfinished building, with construction noises disturbing the surroundings. No on-site gym or pool facilities existed, and no room service was provided. Multiple guests have filed similar complaints, which makes it apparent that the listing was deliberately misleading. \r\nSuch claims could harm our platform's reputation, as users rely on accurate listings to make informed booking decisions. Photos in the listing also did not match the actual rooms, showing much larger spaces than what is realistically available. \r\nWe contacted the host to clarify the situation, but they failed to provide a reasonable explanation for the discrepancy. Given the number of complaints and evidence from recent guests, this report highlights the need to ensure that the listing complies with platform standards and accurately represents what future guests can expect. \r\nWe are temporarily suspending the listing and issuing a warning to the host to revise all false information immediately.",
                Status = ReportStatus.Approved,
                EntityType = EntityType.User,
                HandledByAdminId = 2004,
                HandledDate = DateTime.Now.ToUniversalTime().AddDays(-3),
                AdminNotes = "The user repeatedly messaged another user on our platform, making them feel uncomfortable with unsolicited personal questions. Despite being politely asked to stop, the user continued to contact the reporting party through our messaging system. \r\nIn one instance, the user made unwelcome comments about the reporting user’s photos on their profile, which escalated into making remarks that were perceived as threatening. This behavior directly violates our platform’s code of conduct, which promotes a safe and respectful environment. \r\nAs the messages were not related to the property booking or related queries, they are considered harassment under our community guidelines. The reporting user blocked the offender and raised this report with screenshots as evidence. \r\nGiven the severity of the messages, it was escalated for administrative review. Any form of harassment is taken seriously, and the reported messages are now being reviewed with the intent to enforce consequences that align with our anti-harassment policy."
            },
            new() {
                Id = 8,
                UserId = 8,
                EntityId = 4002,
                ReportReason = "False Information",
                Description = "The property description claimed to offer amenities like a swimming pool, gym, and 24-hour room service, but guests who arrived found these services unavailable. Instead, the property was in an unfinished building, with construction noises disturbing the surroundings. No on-site gym or pool facilities existed, and no room service was provided. Multiple guests have filed similar complaints, which makes it apparent that the listing was deliberately misleading. \r\nSuch claims could harm our platform's reputation, as users rely on accurate listings to make informed booking decisions. Photos in the listing also did not match the actual rooms, showing much larger spaces than what is realistically available. \r\nWe contacted the host to clarify the situation, but they failed to provide a reasonable explanation for the discrepancy. Given the number of complaints and evidence from recent guests, this report highlights the need to ensure that the listing complies with platform standards and accurately represents what future guests can expect. \r\nWe are temporarily suspending the listing and issuing a warning to the host to revise all false information immediately.",
                Status = ReportStatus.Pending,
                EntityType = EntityType.Property
            },
            new() {
                Id = 9,
                UserId = 9,
                EntityId = 3003,
                ReportReason = "Spam",
                Description = "The user repeatedly messaged another user on our platform, making them feel uncomfortable with unsolicited personal questions. Despite being politely asked to stop, the user continued to contact the reporting party through our messaging system. \r\nIn one instance, the user made unwelcome comments about the reporting user’s photos on their profile, which escalated into making remarks that were perceived as threatening. This behavior directly violates our platform’s code of conduct, which promotes a safe and respectful environment. \r\nAs the messages were not related to the property booking or related queries, they are considered harassment under our community guidelines. The reporting user blocked the offender and raised this report with screenshots as evidence. \r\nGiven the severity of the messages, it was escalated for administrative review. Any form of harassment is taken seriously, and the reported messages are now being reviewed with the intent to enforce consequences that align with our anti-harassment policy.",
                Status = ReportStatus.Rejected,
                EntityType = EntityType.Review,
                HandledByAdminId = 2005,
                HandledDate = DateTime.Now.ToUniversalTime().AddDays(-7),
                AdminNotes = "Review flagged but deemed non-disruptive."
            },
            new() {
                Id = 10,
                UserId = 10,
                EntityId = 4004,
                ReportReason = "Inappropriate Content",
                Description = "This review was flagged by several users for including explicit and offensive language, which violates our community guidelines. The user made multiple derogatory remarks about the property host and used highly inappropriate words to describe their stay. \r\nThis review not only reflects poor judgment but also breaches our platform's guidelines on respectful and constructive feedback. Instead of focusing on the specific issues they encountered, the user went on to insult the host’s appearance and personal qualities, which is entirely irrelevant to the property's service and quality. After investigating the report, we found that the feedback did not contribute positively and might have been exaggerated or posted in retaliation. It’s critical that reviews stay professional and fact-based, especially on a platform where other users rely on honest and fair assessments to make booking decisions. \r\nFor these reasons, the review was temporarily hidden while we address the user’s behavior through appropriate channels.",
                Status = ReportStatus.Approved,
                EntityType = EntityType.Property,
                HandledByAdminId = 2006,
                HandledDate = DateTime.Now.ToUniversalTime().AddDays(-4),
                AdminNotes = "Description edited to remove offensive terms."
            }
        ]);
    }
}
