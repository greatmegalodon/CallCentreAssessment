"use client";

import { DefaultPagination, usePaginatedQuery } from "@/hooks/query";
import { useState } from "react";

import { Table } from "../../components/Table";
import Link from "../../components/Link";
import Pagination from "../../components/Pagination";
import { Card } from "@/components/Card";

interface Call {
  id: string;
  username: string;
  dateCallStarted: string;
}

const StatsTable = () => {
  const [page, setPage] = useState(DefaultPagination.page);

  const { data, isLoading } = usePaginatedQuery<Call>(
    "/Call",
    { page },
    { staleTime: 0 }
  );

  const onNext = () => {
    if (page === data?.totalPages) return;

    setPage(page + 1);
  };

  const onPrevious = () => {
    if (page === 1) return;

    setPage(page - 1);
  };

  return (
    <div>
      <div className="flex mb-4">
        <Card>
          <Card.Title>{data?.totalResultsCount}</Card.Title>
          <Card.Body>Total number of Calls</Card.Body>
        </Card>
        <Card>
          <Card.Title>{data?.totalResultsCount}</Card.Title>
          <Card.Body>Total number of Calls</Card.Body>
        </Card>
        <Card>
          <Card.Title>{data?.totalResultsCount}</Card.Title>
          <Card.Body>Total number of Calls</Card.Body>
        </Card>                
      </div>
      <div className="sm:flex sm:items-center">
        <div className="sm:flex-auto">
          <h1 className="text-base font-semibold leading-6 text-white">
            Calls
          </h1>
          <p className="mt-2 text-sm text-gray-300">
            A list of all the calls placed by users in your account.
          </p>
        </div>
        <div className="mt-4 sm:ml-16 sm:mt-0 sm:flex-none">
          <Link href="/calls/create">New Call</Link>
        </div>
      </div>
      <Table.Container>
        {isLoading ? (
          <Table.Loading />
        ) : (
          <Table>
            <Table.Header>
              <Table.HeaderCell>Hour</Table.HeaderCell>
              <Table.HeaderCell>Call Count</Table.HeaderCell>
              <Table.HeaderCell>Top User</Table.HeaderCell>
            </Table.Header>
            <Table.Body>
              {data?.data.map((call) => (
                <Table.Row key={call.id}>
                  <Table.Cell>{call.username}</Table.Cell>
                  <Table.Cell>
                    {new Date(call.dateCallStarted).toDateString()}
                  </Table.Cell>
                  <Table.Cell className="font-medium text-right">
                    <a href="#" className="text-gray-300 hover:text-white">
                      Edit
                    </a>
                  </Table.Cell>
                </Table.Row>
              ))}
            </Table.Body>
          </Table>
        )}
      </Table.Container>
      <Pagination
        page={page}
        totalPages={data?.totalPages}
        onNext={onNext}
        onPrevious={onPrevious}
      />
    </div>
  );
};

export default StatsTable;
