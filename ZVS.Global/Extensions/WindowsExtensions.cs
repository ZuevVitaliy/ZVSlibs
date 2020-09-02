using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ZVS.Global.Extensions
{
    public static class WindowsExtensions
    {
        /// <summary>
        /// Получить первый встреченный родительский элемент заданного типа.
        /// </summary>
        /// <typeparam name="T">Заданый тип элемента.</typeparam>
        /// <param name="element">Дочерний элемент, с которого начнется поиск.</param>
        /// <returns>Родительский элемент заданного типа.</returns>
        public static T GetParentOfType<T>(this DependencyObject element) where T : class
        {
            if (element == null)
                throw new ArgumentNullException("element");

            DependencyObject parent = element.GetParent();
            while (parent != null)
            {
                if (parent is T)
                {
                    return parent as T;
                }

                parent = parent.GetParent();
            }

            return null;
        }

        /// <summary>
        /// Получить все родительские элементы от текущего элемента.
        /// </summary>
        /// <param name="element">Дочерний элемент, с которого начнется поиск.</param>
        /// <returns>Набор родительских элементов.</returns>
        public static IEnumerable<DependencyObject> GetParents(this DependencyObject element)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            while ((element = GetParent(element)) != null)
                yield return element;
        }

        /// <summary>
        /// Получить непосредственный родительсий элемент от текущего элемента.
        /// </summary>
        /// <param name="element">Текущий дочерний элемент.</param>
        /// <returns>Родительсий элемент.</returns>
        public static DependencyObject GetParent(this DependencyObject element)
        {
            DependencyObject dependencyObject;
            try
            {
                dependencyObject = VisualTreeHelper.GetParent(element);
            }
            catch (InvalidOperationException ex)
            {
                dependencyObject = null;
            }
            if (dependencyObject == null)
            {
                FrameworkElement frameworkElement = element as FrameworkElement;
                if (frameworkElement != null)
                    dependencyObject = frameworkElement.Parent;
                FrameworkContentElement frameworkContentElement = element as FrameworkContentElement;
                if (frameworkContentElement != null)
                    dependencyObject = frameworkContentElement.Parent;
            }
            return dependencyObject;
        }

        /// <summary>
        /// Получить дочерние элементы от текущего элемента.
        /// </summary>
        /// <typeparam name="T">Заданный тип элементов.</typeparam>
        /// <param name="element">Родительский элемент, с которого начнется поиск.</param>
        /// <returns>Дочерние элементы заданного типа.</returns>
        public static IEnumerable<T> GetChildrensOfType<T>(this DependencyObject element)
        {
            return element.GetChildrens().OfType<T>();
        }

        /// <summary>
        /// Получить все дочерние элементы от текущего элемента.
        /// </summary>
        /// <param name="element">Родительский элемент, с которого начнется поиск.</param>
        /// <returns>Набор дочерних элементов.</returns>
        public static IEnumerable<DependencyObject> GetChildrens(this DependencyObject element)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(element, i);
                if (child != null)
                {
                    yield return child;
                    foreach (DependencyObject dependencyObject in GetChildrens(child))
                        yield return dependencyObject;
                }
            }
        }
    }
}
