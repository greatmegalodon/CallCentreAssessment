"use client";

import { DefaultPagination, usePaginatedQuery } from "@/hooks/query";
import { useState } from "react";
import { Table } from "../../components/Table";
import Pagination from "../../components/Pagination";
import { Card } from "@/components/Card";
import { useMemo } from "react";
import internal from "stream";

interface Stat {
  id: string;
  username: string;
  dateCallStarted: string;
  mostCallsDate: string;
  averageCallsPerDay: number;
  averageCallsPerUser: number;
}

const StatsTable = () => {
    const [page, setPage] = useState(DefaultPagination.page);
    let mostDateCalls: string = "";
    let avgCallsPerDay: number = 0;
    let avgCallsPerUser: number = 0;
    const { data, isLoading } = usePaginatedQuery<Stat>(
      "/Stats",
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
  
    const formatTimePerHour = (time: string): string => {
      const date = new Date(time);
      let hours = date.getHours();
      const ampm = hours >= 12 ? 'PM' : 'AM';
      return `${hours % 12 || 12}:00 ${ampm}`;
    };
  
    const groupedData = useMemo(() => {
        const groups: Record<string, Stat[]> = {};
        data?.data.forEach(call => {
          const hour = formatTimePerHour(call.dateCallStarted);
          if (!groups[hour]) {
            groups[hour] = [];
          }
          groups[hour].push(call);
        });
        return groups;
      }, [data]);

    const getWorkHours = Array.from({ length: 9 }, (_, i) => {
        const hour = i + 9; // Start from 9 AM
        const ampm = hour >= 12 ? 'PM' : 'AM';
        return `${hour % 12 || 12}:00 ${ampm}`;
      });    
  
    return (
        <div>
          <div className="flex mb-4">
            <Card>
              <Card.Title>{mostDateCalls}</Card.Title>
              <Card.Body>Most Calls Date</Card.Body>
            </Card>
            <Card>
              <Card.Title>{avgCallsPerDay.toString()}</Card.Title>
              <Card.Body>Average Number of Calls Per Day</Card.Body>
            </Card>
            <Card>
              <Card.Title>{avgCallsPerUser.toString()}</Card.Title>
              <Card.Body>Average Number of Calls Per User</Card.Body>
            </Card>
          </div>
          <div className="sm:flex sm:items-center">
            <div className="sm:flex-auto">
              <h1 className="text-base font-semibold leading-6 text-white">
                Stats
              </h1>
              <p className="mt-2 text-sm text-gray-300">
                Display of all the statistics available
              </p>
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
              {getWorkHours.map(hour => {
                const calls = groupedData[hour] || [];
                const userCallCount: Record<string, number> = {};
                calls.forEach(call => {
                  userCallCount[call.username] = (userCallCount[call.username] || 0) + 1;
                  mostDateCalls = call.mostCallsDate;
                  avgCallsPerDay = call.averageCallsPerDay;
                  avgCallsPerUser = call.averageCallsPerUser;
                });
                const maxCallCount = Math.max(...Object.values(userCallCount));
                const topUsers = Object.entries(userCallCount)
                  .filter(([_, count]) => count === maxCallCount)
                  .map(([username]) => username);
                
                return (
                  <Table.Row key={hour}>
                    <Table.Cell>{hour}</Table.Cell>
                    <Table.Cell>{calls.length} Calls</Table.Cell>
                    <Table.Cell>
                      {topUsers.length > 0 ? topUsers.join(', ') : 'No calls'}
                    </Table.Cell>
                  </Table.Row>
                );
              })}
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