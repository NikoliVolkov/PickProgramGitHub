// Write your JavaScript code.
(function ($, F) {

	/**
	 * An object containing the Paging Widget options.
	 * @type {object}
	 * @prop {boolean} enabled - Whether or not to enable the paging widget.
	 * @prop {string} countFormat - A string format used to generate the page count text.
	 */
    F.Defaults.prototype.paging.widget = {
        enabled: false,
        countFormat: "Showing {PF}-{PL} of {TR} results"
    };

    F.PagingWidget = F.Component.extend(/** @lends FooTable.Component */{
		/**
		 * The constructor for the Paging Widget.
		 * @constructs
		 * @extends FooTable.Component
		 * @param {FooTable.Table} table - The parent {@link FooTable.Table} object for the component.
		 */
        construct: function (table) {
            // call the base constructor
            this._super(table, table.o.paging.widget.enabled);
			/**
			 * The string format used to generate the page count text.
			 * @type {string}
			 */
            this.countFormat = table.o.paging.widget.countFormat;
			/**
			 * A reference to the FooTable.Paging component for this instance of the plugin.
			 * @type {?FooTable.Paging}
			 */
            this.paging = null;
			/**
			 * A reference to the FooTable.Filtering component for this instance of the plugin.
			 * @type {?FooTable.Filtering}
			 */
            this.filtering = null;
			/**
			 * Whether or not filtering is included and enabled for this instance of the plugin.
			 * @type {boolean}
			 */
            this.filtered = false;
			/**
			 * The jQuery TR object the widget uses for its elements.
			 * @type {?jQuery}
			 */
            this.$row = null;
			/**
			 * The jQuery TH object the widget uses for its elements.
			 * @type {?jQuery}
			 */
            this.$cell = null;
			/**
			 * The jQuery DIV object the widget uses to wrap its elements.
			 * @type {?jQuery}
			 */
            this.$container = null;
			/**
			 * The jQuery SPAN object the widget uses to display the current page or row count specified by the {@link FooTable.PagingWidget#countFormat} value.
			 * @type {?jQuery}
			 */
            this.$count = null;
			/**
			 * The jQuery BUTTON object the widget uses for its previous button.
			 * @type {?jQuery}
			 */
            this.$prev = null;
			/**
			 * The jQuery BUTTON object the widget uses for its next button.
			 * @type {?jQuery}
			 */
            this.$next = null;
        },
		/**
		 * Checks the supplied data and options for the paging widget component.
		 * @instance
		 * @param {object} data - The jQuery data object from the parent table.
		 */
        preinit: function (data) {
            this.paging = this.ft.use(F.Paging);
            this.filtering = this.ft.use(F.Filtering);
            this.enabled = F.is.boolean(data.pagingWidget)
                ? data.pagingWidget
                : this.enabled && this.paging.enabled;
            if (!this.enabled) return;
            this.filtered = this.filtering && this.filtering.enabled;
            this.countFormat = !F.is.emptyString(data.pagingWidgetCountFormat) ? data.pagingWidgetCountFormat : this.countFormat;
        },
		/**
		 * Initializes the paging widget component for the plugin.
		 * @instance
		 */
        init: function () {
            if (this.filtered) {
                // if the filtering component is included and enabled use its' row and form
                this.$row = this.filtering.$row.addClass('footable-paging-widget-row');
                this.$cell = this.filtering.$cell;
                this.$container = $('<div/>', { 'class': 'form-group footable-paging-widget' }).prependTo(this.$cell.find('.form-inline'));
            } else {
                // otherwise create our own for the component
                this.$row = $('<tr/>', { 'class': 'footable-paging-widget-row' }).prependTo(this.ft.$el.children('thead'));
                this.$cell = $('<td/>').appendTo(this.$row);
                this.$container = $('<div/>', { 'class': 'footable-paging-widget' }).appendTo(this.$cell);
            }
            var $prevIcon = $('<span/>', { 'class': 'glyphicon glyphicon-menu-left' });
            var $nextIcon = $('<span/>', { 'class': 'glyphicon glyphicon-menu-right' });
            this.$count = $('<span/>', { 'class': 'footable-paging-widget-label' });
            this.$prev = $('<button/>', { 'class': 'btn btn-default', type: 'button' }).on('click', { self: this }, this.onPrevClick).append($prevIcon);
            this.$next = $('<button/>', { 'class': 'btn btn-default', type: 'button' }).on('click', { self: this }, this.onNextClick).append($nextIcon);
            this.$group = $('<div/>', { 'class': 'btn-group' }).append(this.$prev, this.$next);
            //this.$container.append(this.$count, this.$group);
            this.$container.append(this.$count);
        },
		/**
		 * Destroys the paging widget component removing all traces of it from the DOM.
		 */
        destroy: function () {
            this.$container.remove();
            if (this.filtered) {
                // if the filtering component is included and enabled just remove the custom widget class from the row
                this.$row.removeClass('footable-paging-widget-row');
            } else {
                // otherwise remove the whole row
                this.$row.remove();
            }
        },
		/**
		 * Updates the paging widget component UI.
		 * @instance
		 */
        draw: function () {
            this.$count.text(this.paging.format(this.countFormat));
            if (!this.filtered) {
                // if we created our own row we need to update the colspan otherwise it is left up to the filtering component
                this.$cell.attr('colspan', this.ft.columns.visibleColspan);
            }
        },
        onPrevClick: function (e) {
            e.data.self.paging.prev();
        },
        onNextClick: function (e) {
            e.data.self.paging.next();
        }
    });

    // register the paging widget component with a priority of 300 so that it executes after the filtering and paging components have.
    F.components.register('paging_widget', F.PagingWidget, 300);

})(jQuery, FooTable);