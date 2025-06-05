import 'package:flutter/material.dart';

class DayBox extends StatefulWidget {
  final String day;
  final bool selected;
  final ValueChanged<bool> onChanged;

  const DayBox({
    super.key,
    required this.day,
    required this.selected,
    required this.onChanged,
  });

  @override
  State<DayBox> createState() => _DayBoxState();
}

class _DayBoxState extends State<DayBox> {
  late bool _selected;

  @override
  void initState() {
    super.initState();
    _selected = widget.selected;
  }

  @override
  void didUpdateWidget(DayBox oldWidget) {
    super.didUpdateWidget(oldWidget);
    if (oldWidget.selected != widget.selected) {
      setState(() => _selected = widget.selected);
    }
  }

  @override
  Widget build(BuildContext context) {
    final colorScheme = Theme.of(context).colorScheme;
    final textTheme = Theme.of(context).textTheme;

    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 6, vertical: 6),
      child: AnimatedContainer(
        duration: const Duration(milliseconds: 200),
        decoration: BoxDecoration(
          color: _selected ? colorScheme.primary : colorScheme.surface,
          borderRadius: BorderRadius.circular(12),
          border: Border.all(
            color: _selected ? colorScheme.primary : colorScheme.outline,
            width: 1.5,
          ),
          boxShadow: _selected
              ? [
                  BoxShadow(
                    color: colorScheme.primary.withOpacity(0.25),
                    blurRadius: 4,
                    offset: const Offset(0, 2),
                  )
                ]
              : [],
        ),
        child: InkWell(
          borderRadius: BorderRadius.circular(12),
          onTap: () {
            setState(() => _selected = !_selected);
            widget.onChanged(_selected);
          },
          child: Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
            child: Text(
              widget.day,
              style: textTheme.labelLarge?.copyWith(
                fontWeight: FontWeight.w600,
                color:
                    _selected ? colorScheme.onPrimary : colorScheme.onSurface,
              ),
              textAlign: TextAlign.center,
            ),
          ),
        ),
      ),
    );
  }
}
