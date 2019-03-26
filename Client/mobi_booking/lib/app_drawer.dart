import 'package:flutter/material.dart';
import './pages.dart';

class AppDrawer extends StatelessWidget {
  final void Function(Pages) _nawigateToPage;

  AppDrawer(this._nawigateToPage);

  Widget _drawHeader() {
    return DrawerHeader(
      child: Text('Drawer Header'),
      decoration: BoxDecoration(
        color: Color(0xff002132),
      ),
    );
  }

  Widget _drawElement(String text, Pages page, BuildContext context) {
    return Container(
        margin: EdgeInsets.all(10),
        color: Color(0xff002132),
        child: ListTile(
          title: Text(
            text,
            style: TextStyle(
                color: Colors.white, fontWeight: FontWeight.bold, fontSize: 20),
          ),
          onTap: () {
            _nawigateToPage(page);
            Navigator.pop(context);
          },
        ));
  }

  Widget _drawItem(int index, BuildContext context) {
    switch (index) {
      case 0:
       return _drawHeader();

       case 1:
       return _drawElement("Dashboard", Pages.dashboard, context);

       case 2:
       return _drawElement("Zarezerwuj salę", Pages.bookRoom, context);

       case 3:
       return _drawElement("Lista sal / rezerwacje", Pages.dashboard, context);

       case 4:
       return _drawElement("Dodaj salę", Pages.addRoom, context);

       case 5:
       return _drawElement("Dodaj użytkownika", Pages.addUser, context);

       case 6:
       return _drawElement("Użytkownicy", Pages.addUser, context);

      default: return null;
    }
  }

  @override
  Widget build(BuildContext context) {
    return new Drawer(
      child: ListView.builder(
        padding: EdgeInsets.zero,
        itemCount: 6,
        itemBuilder: (item, index) {
          return _drawItem(index, context);
        },
      ),
    );
  }
}
