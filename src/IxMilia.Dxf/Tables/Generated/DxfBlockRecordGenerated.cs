// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

// The contents of this file are automatically generated by a tool, and should not be directly modified.

using System.Linq;
using System.Collections.Generic;
using IxMilia.Dxf.Sections;
using IxMilia.Dxf.Tables;

namespace IxMilia.Dxf
{
    public partial class DxfBlockRecord : DxfSymbolTableFlags
    {
        internal const string AcDbText = "AcDbBlockTableRecord";

        protected override DxfTableType TableType { get { return DxfTableType.BlockRecord; } }

        public uint LayoutHandle { get; set; }
        public DxfUnits InsertionUnits { get; set; }
        public bool Explodability { get; set; }
        public bool Scalability { get; set; }
        private List<string> _bitmapPreviewData { get; set; }

        public DxfXData XData { get; set; }

        public DxfBlockRecord()
            : base()
        {
            LayoutHandle = 0u;
            InsertionUnits = DxfUnits.Unitless;
            Explodability = true;
            Scalability = true;
            _bitmapPreviewData = new List<string>();
        }

        internal override void AddValuePairs(List<DxfCodePair> pairs, DxfAcadVersion version, bool outputHandles)
        {
            if (version >= DxfAcadVersion.R13)
            {
                pairs.Add(new DxfCodePair(100, AcDbText));
            }

            pairs.Add(new DxfCodePair(2, Name));
            if (LayoutHandle != 0u && version >= DxfAcadVersion.R2000)
            {
                pairs.Add(new DxfCodePair(340, UIntHandle(LayoutHandle)));
            }

            if (version >= DxfAcadVersion.R2007)
            {
                pairs.Add(new DxfCodePair(70, (short)(InsertionUnits)));
            }

            if (version >= DxfAcadVersion.R2007)
            {
                pairs.Add(new DxfCodePair(280, BoolShort(Explodability)));
            }

            if (version >= DxfAcadVersion.R2007)
            {
                pairs.Add(new DxfCodePair(281, BoolShort(Scalability)));
            }

            if (version >= DxfAcadVersion.R2000)
            {
                pairs.AddRange(_bitmapPreviewData.Select(value => new DxfCodePair(310, value)));
            }

            if (XData != null)
            {
                XData.AddValuePairs(pairs, version, outputHandles);
            }
        }

        internal static DxfBlockRecord FromBuffer(DxfCodePairBufferReader buffer)
        {
            var item = new DxfBlockRecord();
            while (buffer.ItemsRemain)
            {
                var pair = buffer.Peek();
                if (pair.Code == 0)
                {
                    break;
                }

                buffer.Advance();
                switch (pair.Code)
                {
                    case DxfCodePairGroup.GroupCodeNumber:
                        var groupName = DxfCodePairGroup.GetGroupName(pair.StringValue);
                        item.ExtensionDataGroups.Add(DxfCodePairGroup.FromBuffer(buffer, groupName));
                        break;
                    case 340:
                        item.LayoutHandle = UIntHandle(pair.StringValue);
                        break;
                    case 70:
                        item.InsertionUnits = (DxfUnits)(pair.ShortValue);
                        break;
                    case 280:
                        item.Explodability = BoolShort(pair.ShortValue);
                        break;
                    case 281:
                        item.Scalability = BoolShort(pair.ShortValue);
                        break;
                    case 310:
                        item._bitmapPreviewData.Add((pair.StringValue));
                        break;
                    case (int)DxfXDataType.ApplicationName:
                        item.XData = DxfXData.FromBuffer(buffer, pair.StringValue);
                        break;
                    default:
                        item.TrySetPair(pair);
                        break;
                }
            }

            return item;
        }
    }
}