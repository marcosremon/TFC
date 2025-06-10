import 'package:flutter/material.dart';
import 'package:modal_bottom_sheet/modal_bottom_sheet.dart';
import 'package:tfc_gym_app/presentation/widgets/other/how_to_use_app_bottom_sheet_content.dart';

class HowToUseAppBottomSheet {
  static Future<void> show(BuildContext context) {
    return showMaterialModalBottomSheet(
      context: context,
      expand: false,
      enableDrag: true,
      bounce: true,
      duration: const Duration(milliseconds: 400),
      builder: (context) => DraggableScrollableSheet(
        expand: false,
        initialChildSize: 0.75,
        minChildSize: 0.5,
        maxChildSize: 0.9,
        builder: (context, scrollController) {
          return Container(
            decoration: BoxDecoration(
              color: Theme.of(context).scaffoldBackgroundColor,
              borderRadius: const BorderRadius.only(
                topLeft: Radius.circular(25),
                topRight: Radius.circular(25),
              ),
              boxShadow: [
                BoxShadow(
                  color: Colors.black26,
                  blurRadius: 10,
                  offset: Offset(0, -5),
                ),
              ],
            ),
            child: HowToUseAppBottomSheetContent(
              scrollController: scrollController,
            ),
          );
        },
      ),
    );
  }
}
