﻿using Tuxboard.Core.Domain.Entities;

namespace DefaultDashboards.Pages.Shared.Components.AdvancedLayoutDialog;

public class AdvancedLayoutModel
{
    public List<LayoutRow> LayoutRows { get; set; } = new();
    public List<LayoutType> LayoutTypes { get; set; } = new();
}