import { Component, ViewChild } from '@angular/core';

@Component({
  selector: 'tabs-containers',
  templateUrl: 'tabs-container-page.component.html',
  styleUrls: ['tabs-container-page.component.scss'],
})
export class TabsContainerPage {
  selectedTab = '';
  @ViewChild('tabs', { static: true }) tabs: any;

  async setCurrentTab() {
    this.selectedTab = this.tabs.getSelected();
  }
}
