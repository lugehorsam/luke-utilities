namespace Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    using UnityEngine;

    public class RectSearch
    {
        private Rect _masterRect;
        private readonly IEnumerable<Rect> _occupantRects;
        private readonly float _searchSize;

        public RectSearch(Rect masterRect, IEnumerable<Rect> occupantRects, float searchSize)
        {
            _masterRect = masterRect;
            _occupantRects = occupantRects;
            _searchSize = searchSize;
        }

        public Vector2? GetAvailablePoint()
        {
            Rect entireRect = _masterRect.Shrink(2f);
            Rect? availableRect = GetAvailableRect(new[] {entireRect});
            return availableRect?.GetRandomPoint();
        }

        private Rect? GetAvailableRect(IEnumerable<Rect> rectsToSearch)
        {
            if (!rectsToSearch.Any())
            {
                return null;
            }

            rectsToSearch = rectsToSearch.SelectMany(rect => rect.CreateQuadrants());

            IEnumerable<Rect> availableRects = rectsToSearch.Where(IsRectAvailable);

            if (availableRects.Any())
            {
                return availableRects.GetRandomElement();
            }

            IEnumerable<Rect> unavailableRects = rectsToSearch.Except(availableRects);

            IEnumerable<Rect> newRectsToSearch = unavailableRects.Where(RectSizeIsSearchable);

            return GetAvailableRect(newRectsToSearch);
        }

        private bool IsRectAvailable(Rect rect)
        {
            foreach (Rect occupantRect in _occupantRects)
            {
                if (rect.Overlaps(occupantRect))
                {
                    return false;
                }
            }

            return true;
        }

        private bool RectSizeIsSearchable(Rect rect)
        {
            return rect.width > _searchSize;
        }
    }
}
