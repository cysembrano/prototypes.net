using FlowFB.Data;
using FlowFB.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowFB.BusinessLogic.Extensions
{
    public static class FFBA_Projects_Extension
    {
        public static FBProject[] ToFBProjects(this IEnumerable<FFBA_Projects> entities)
        {
            return entities.Select(d => d.ToFBProject()).ToArray();
        }

        public static FBProject ToFBProject(this FFBA_Projects entity)
        {
            return new FBProject()
            {
                ProjectID = entity.ProjectID,
                ProjectName = entity.ProjectName,
                AllowFileCreateOnBatch = entity.AllowFileCreateOnBatch,
                AllowManualDeclaration = entity.AllowManualDeclaration,
                AllowUnsign  = entity.AllowUnsign,
                ArchiveEmail = entity.ArchiveEmail,
                Boxes = entity.Boxes,
                CAR = entity.CAR,
                DividerSecurity = entity.DividerSecurity,
                DivLabel = entity.DivLabel,
                DivSortDescending = entity.DivSortDescending,
                DocuSignEnabled = entity.DocuSignEnabled,
                FBDrive = entity.FBDrive,
                FieldSecurity = entity.FieldSecurity,
                FileRoomEmails = entity.FileRoomEmails,
                FileTracking = entity.FileTracking,
                ForceBox = entity.ForceBox,
                FullText = entity.FullText,
                KeepBox = entity.KeepBox,
                Hidden = entity.Hidden,
                Imaging = entity.Imaging,
                ImportNewToLabelManager = entity.ImportNewToLabelManager,
                IncludeLineItemSearch = entity.IncludeLineItemSearch,
                IndexDividers = entity.IndexDividers,
                IndexSeparators = entity.IndexSeparators,
                InteractiveRevisions = entity.InteractiveRevisions,
                LabelAlertMin = entity.LabelAlertMin,
                LabelCheckInterval = entity.LabelCheckInterval,
                LabelManager = entity.LabelManager,
                LabelManagerFolder = entity.LabelManagerFolder,
                LastUpdated = entity.LastUpdated,
                LastUpdatedBy = entity.LastUpdatedBy,
                LockDiv = entity.LockDiv,
                Locking = entity.Locking,
                LockSep = entity.LockSep,
                MultiKey = entity.MultiKey,
                MultiLevel = entity.MultiLevel,
                NativePDF = entity.NativePDF,
                NewDocRoute = entity.NewDocRoute,
                NewDocRouteAction = entity.NewDocRouteAction,
                NoPaper = entity.NoPaper, 
                NumRevisions = entity.NumRevisions,
                PageNumbers = entity.PageNumbers,
                PageSize = entity.PageSize,
                ProjectType = entity.ProjectType,
                QueueSelect = entity.QueueSelect,
                Reminders = entity.Reminders,
                RemoteID = entity.RemoteID,
                RenditionAction = entity.RenditionAction,
                RenditionMaximumDimension = entity.RenditionMaximumDimension,
                RenditionPerformed = entity.RenditionPerformed,
                ReportView = entity.ReportView,
                Revisions = entity.Revisions,
                SaveStyle = entity.SaveStyle,
                ScanEmails = entity.ScanEmails,
                SearchPortal = entity.SearchPortal,
                SearchPortalSecurityMode = entity.SearchPortalSecurityMode,
                SecurityField = entity.SecurityField,
                SeparatorSecurity = entity.SeparatorSecurity,
                SepLabel = entity.SepLabel,
                SepSortDescending = entity.SepSortDescending,
                ServerID = entity.ServerID,
                ServerOCRSkipExtensions = entity.ServerOCRSkipExtensions,
                ServerOCRType = entity.ServerOCRType,
                ShowChangeDate = entity.ShowChangeDate,
                TaskManager = entity.TaskManager

            };
        }
    }
}
