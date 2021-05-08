import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';

interface Node {
  id: number;
  name: string;
  children?: Node[];
}

let nodes: Node[] = []

const loadNodesFromSellers = (sellers: Seller[]) =>
{
  console.log("Sellers are ")
  console.log(sellers)

  let nodeMap = {}

  for (let seller of sellers) {
    nodeMap[seller.id] = { id: seller.id, bossId: seller.bossId, name: `${seller.firstName} ${seller.lastName}`, children: [] }
  }

  for (let seller of sellers) {
    if (seller.bossId != seller.id) nodeMap[seller.bossId].children.push(nodeMap[seller.id])
  }

  console.log("Node map is ")
  console.log(nodeMap)

  for (let key in nodeMap) if (nodeMap[key].bossId == key) nodes.push(nodeMap[key])

  console.log("Nodes are")
  console.log(nodes)
}

@Component({
  selector: 'fetch-sellers',
  templateUrl: './fetch-sellers.component.html',
  styleUrls: ['./fetch-sellers.component.css']
})
export class FetchSellersComponent {
  public loading: boolean
  private loader: () => Subscription
  treeControl = new NestedTreeControl<Node>(node => node.children);
  dataSource = new MatTreeNestedDataSource<Node>();

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.loading = false
    this.loader = () => http.get<Seller[]>(baseUrl + 'api/sellers').subscribe(result => {
      loadNodesFromSellers(result);
      this.dataSource.data = nodes
      this.loading = false
    }, error => console.error(error));

    this.load()
  }

  load() {
    this.loading = true
    this.loader()
  }

  hasChild = (_: number, node: Node) => !!node.children && node.children.length > 0;
}

interface Seller {
  id: number,
  bossId: number,
  firstName: string,
  lastName: string
}
