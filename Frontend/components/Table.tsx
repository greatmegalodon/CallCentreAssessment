"use client";

import classMerge from "@/util/classmerge";

const TableLoading = () => {
  return (
    <div className="flex justify-center text-sm text-gray-300">Loading...</div>
  );
};

interface TableHeaderCellProps
  extends React.PropsWithChildren<React.HTMLAttributes<HTMLTableCellElement>> {}

const TableHeaderCell = ({ children, ...props }: TableHeaderCellProps) => {
  return (
    <th
      scope="col"
      className="py-3.5 pl-4 pr-3 text-left text-sm font-semibold text-white sm:pl-0"
      {...props}
    >
      {children}
    </th>
  );
};

interface TableHeaderProps
  extends React.PropsWithChildren<
    React.HTMLAttributes<HTMLTableSectionElement>
  > {}

const TableHeader = ({ children, ...props }: TableHeaderProps) => {
  return (
    <thead {...props}>
      <tr>{children}</tr>
    </thead>
  );
};

interface TableRowProps
  extends React.PropsWithChildren<React.HTMLAttributes<HTMLTableRowElement>> {}

const TableRow = ({ children, ...props }: TableRowProps) => {
  return <tr {...props}>{children}</tr>;
};

interface TableBodyProps
  extends React.PropsWithChildren<
    React.HTMLAttributes<HTMLTableSectionElement>
  > {}

const TableBody = ({ children, ...props }: TableBodyProps) => {
  return (
    <tbody className="divide-y divide-gray-800" {...props}>
      {children}
    </tbody>
  );
};

interface TableCellProps
  extends React.PropsWithChildren<React.HTMLAttributes<HTMLTableCellElement>> {
  className?: string;
}

const TableCell = ({ children, className, ...props }: TableCellProps) => {
  return (
    <td
      className={classMerge(
        "whitespace-nowrap py-4 pl-4 pr-3 text-sm font-medium text-white sm:pl-0",
        className
      )}
      {...props}
    >
      {children}
    </td>
  );
};

interface TableProps
  extends React.PropsWithChildren<React.HTMLAttributes<HTMLTableElement>> {}

const TableRoot = ({ children }: TableProps) => {
  return (
    <table className="min-w-full divide-y divide-gray-700">{children}</table>
  );
};

interface TableContainerProps
  extends React.PropsWithChildren<React.HTMLAttributes<HTMLDivElement>> {}

const TableContainer = ({ children, ...props }: TableContainerProps) => {
  return (
    <div className="mt-6 flow-root" {...props}>
      <div className="-mx-4 -my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
        <div className="inline-block min-w-full py-2 align-middle sm:px-6 lg:px-8">
          {children}
        </div>
      </div>
    </div>
  );
};

export let Table = Object.assign(TableRoot, {
  Loading: TableLoading,
  Container: TableContainer,
  Header: TableHeader,
  HeaderCell: TableHeaderCell,
  Body: TableBody,
  Row: TableRow,
  Cell: TableCell,
});
